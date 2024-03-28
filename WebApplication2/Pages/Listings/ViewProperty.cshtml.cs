using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace FireBnBWeb.Pages.Listings
{
    //Still need to do:
    //1. Make it when discount do apply to a user,
    //such a the NewUser discount only do it for new user of a week
    //2. Add a calendar with showing all free dates
    //3. On html, make location work
    //4. Adding reviews
    //5. Make at least look nice
    public class ViewPropertyModel : PageModel
    {
        private readonly UnitofWork _unitOfWork;

        public Property Property { get; private set; }
        public List<Image> Images { get; private set; }
        public int TotalBedCount { get; private set; }
        public List<PropertyBedConfiguration> PropertyBedConfigurations { get; private set; }
        public List<Amenity> Amenities { get; set; }
        public ApplicationUser Lister { get; private set; }
        public DateTime? EarliestCheckinDate { get; set; }
        public IEnumerable<DateTime> AvailableDates { get; set; }
        public List<PropertyNightlyPrice> NightlyPrices { get; private set; }
        public float PriceForSevenNights { get; private set; }
        public List<PropertyFee> PropertyFees { get; private set; }
        public float CityTax { get; private set; }
        public float CountyTax { get; private set; }
        public float StateTax { get; private set; }
        public float TotalLocationTax { get; private set; }
        public List<PropertyDiscount> PropertyDiscounts { get; private set; }
        public float TotalPriceForSevenNights { get; private set; }




        public ViewPropertyModel(UnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet(int id)
        {
            Property = _unitOfWork.Property.GetById(id);
            if (Property == null)
            {
                return NotFound(); // Return a 404 Not Found error if the property with the specified ID is not found
            }

            // Fetch images associated with the property
            Images = _unitOfWork.Image.GetAll(i => i.PropertyId == id).ToList();

            // Calculate total bed count for the property
            TotalBedCount = _unitOfWork.PropertyBedConfiguration
                .GetAll(pbc => pbc.PropertyId == id, null, "BedConfiguration")
                .Sum(pbc => pbc.BedConfiguration.Quantity);

            // Fetch bed configurations associated with the property
            PropertyBedConfigurations = _unitOfWork.PropertyBedConfiguration
                .GetAll(pbc => pbc.PropertyId == id, null, "BedConfiguration")
                .ToList();

            // Fetch amenities associated with the property
            Amenities = _unitOfWork.PropertyAmenity.GetAmenitiesByPropertyId(id).ToList();

            // Fetch the lister who owns the property
            if (Property.ListerId != null)
            {
                // Fetch the lister who owns the property
                Lister = _unitOfWork.ApplicationUser.GetById(Property.ListerId!);
            }

            // Fetch the earliest check-in date for the property
            EarliestCheckinDate = GetEarliestCheckinDate(id);

            // Calculate available dates for the property
            AvailableDates = GetAvailableDates(id);

            // Fetch nightly prices associated with the property
            NightlyPrices = _unitOfWork.PropertyNightlyPrice
                .GetAll(pnp => pnp.PropertyId == id, includes: "PriceRange")
                .ToList();
            // Calculate price for 7 nights
            PriceForSevenNights = NightlyPrices.Sum(nightlyPrice => nightlyPrice.Rate * 7);

            // Fetch fees associated with the property
            PropertyFees = _unitOfWork.PropertyFee
                .GetAll(pf => pf.PropertyId == id, includes: "Fee")
                .ToList();

            // Fetch location details for the property
            var location = _unitOfWork.Location.Get(l => l.Id == id, includes: "City,County,State");

            if (location != null)
            {
                CityTax = location.City?.TaxRate ?? 0;
                CountyTax = location.County?.TaxRate ?? 0;
                StateTax = location.State?.TaxRate ?? 0;

                // Calculate total location tax
                TotalLocationTax = CityTax + CountyTax + StateTax;
            }

            // Fetch discounts associated with the property
            PropertyDiscounts = _unitOfWork.PropertyDiscount
                .GetAll(pd => pd.PropertyId == id, includes: "Discount")
                .ToList();

            // Calculate total price for 7 nights
            double totalPriceForSevenNights = PriceForSevenNights;

            // Add fees to the total price
            totalPriceForSevenNights += PropertyFees.Sum(fee => fee.Fee.Percentage ?? 0.0);

            // Calculate total tax
            double totalTax = totalPriceForSevenNights * (CityTax + CountyTax + StateTax);

            // Subtract discounts from the total price
            totalPriceForSevenNights -= PropertyDiscounts.Sum(discount => discount.Discount.Value);

            // Calculate final total price for 7 nights and round to 2 decimal points
            TotalPriceForSevenNights = (float)Math.Round(totalPriceForSevenNights + totalTax, 2);



            return Page();
        }
        private DateTime? GetEarliestCheckinDate(int propertyId)
        {
            var earliestBooking = _unitOfWork.Booking.GetAll(b => b.PropertyId == propertyId)
                                                     .OrderBy(b => b.Checkin)
                                                     .FirstOrDefault();
            return earliestBooking?.Checkin;
        }

        private IEnumerable<DateTime> GetAvailableDates(int propertyId)
        {
            var bookings = _unitOfWork.Booking.GetAll(b => b.PropertyId == propertyId)
                                               .OrderBy(b => b.Checkin)
                                               .ToList();

            if (!bookings.Any())
            {
                // If no bookings are found, the property is available indefinitely
                return GetDatesInRange(DateTime.Today, DateTime.Today.AddYears(1)); // Adjust the range as needed
            }

            var currentDate = bookings.First().Checkin.Date;
            var availableDates = new List<DateTime>();

            foreach (var booking in bookings)
            {
                if (currentDate < booking.Checkin.Date)
                {
                    // If there's a gap between the current date and the check-in date of the next booking, add the available dates
                    availableDates.AddRange(GetDatesInRange(currentDate, booking.Checkin.Date.AddDays(-1)));
                }

                currentDate = booking.Checkout.Date.AddDays(1);
            }

            // Add available dates after the last booking
            if (currentDate <= DateTime.Today.AddYears(1)) // Adjust the range as needed
            {
                availableDates.AddRange(GetDatesInRange(currentDate, DateTime.Today.AddYears(1))); // Adjust the range as needed
            }

            return availableDates;
        }

        private IEnumerable<DateTime> GetDatesInRange(DateTime startDate, DateTime endDate)
        {
            var dates = new List<DateTime>();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                dates.Add(date);
            }
            return dates;
        }
    }
}
