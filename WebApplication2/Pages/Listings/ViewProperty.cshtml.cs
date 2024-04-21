using DataAccess;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Utility;

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
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationUser _user;

        /* FROM REVIEW */
        public IEnumerable<Property> objProperties;
        public IEnumerable<Property> Properties { get; private set; }
        public IEnumerable<Booking> bookingList;
        public Review objUserReview { get; set; }
        public Booking objBooking { get; set; }
        public Property objProperty { get; set; }


        /* GET & SET FOR VIEWPROPERTY */
        public Property Property { get; private set; }
        public List<Image> Images { get; private set; }
        public int TotalBedCount { get; private set; }
        public List<PropertyBedConfiguration> PropertyBedConfigurations { get; private set; }
        public List<Amenity> Amenities { get; set; }
        public ApplicationUser Lister { get; private set; }
        public DateTime? EarliestCheckinDate { get; set; }
        public IEnumerable<DateTime> AvailableDates { get; set; }
        public IEnumerable<DateTime> UnavailableDates { get; set; }
        public List<PropertyNightlyPrice> NightlyPrices { get; private set; }
        public float PriceForSevenNights { get; private set; }
        public List<PropertyFee> PropertyFees { get; private set; }
        public float CityTax { get; private set; }
        public float CountyTax { get; private set; }
        public float StateTax { get; private set; }
        public float TotalLocationTax { get; private set; }
        public List<PropertyDiscount> PropertyDiscounts { get; private set; }
        public float TotalPriceForSevenNights { get; private set; }


        /* GET & SET FOR REVIEW */
        [BindProperty]
        public int Rating { get; set; }

        [BindProperty]
        public string Comment { get; set; }

        [BindProperty]
        public DateTime Timestamp { get; set; }

        [BindProperty]
        public bool ReviewType { get; set; }

        [BindProperty]
        public List<int> ReviewStars { get; set; }

        [BindProperty]
        public string userComment { get; set; }

        public bool isRenter { get; set; }


        public ViewPropertyModel(UnitofWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            objUserReview = new Review();
            objBooking = new Booking();
            objProperty = new Property();
        }

        public IActionResult OnGet(int id)
        {
            // Fetch the property along with its location and related entities
            Property = _unitOfWork.Property.GetAllWithLocationsCitiesCountiesStates().FirstOrDefault(p => p.Id == id);
            if (Property == null)
            {
                return NotFound(); // Return a 404 Not Found error if the property with the specified ID is not found
            }

            isRenter = false;
            if (User.IsInRole(SD.RenterRole))
            {
                isRenter = true;
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
            UnavailableDates = GetUnavailableDates(id);

            // Fetch nightly prices associated with the property
            NightlyPrices = _unitOfWork.PropertyNightlyPrice
                .GetAll(pnp => pnp.PropertyId == id)
                //.GetAll(pnp => pnp.PropertyId == id, includes: "PriceRange")
                .ToList();
            // Calculate price for 7 nights
            PriceForSevenNights = NightlyPrices.Sum(nightlyPrice => nightlyPrice.Rate * 7);

            // Fetch fees associated with the property
            PropertyFees = _unitOfWork.PropertyFee
                .GetAll(pf => pf.PropertyId == id, includes: "Fee")
                .ToList();

            // Fetch location details for the property
            var location = _unitOfWork.Property.Get(p => p.Id == id, includes: "City,County,State");

            if (location != null)
            {
                CityTax = location.City?.TaxRate ?? 0;
                CountyTax = location.County?.TaxRate ?? 0;
                StateTax = location.State?.TaxRate ?? 0;

                // Calculate total location tax
                TotalLocationTax = CityTax + CountyTax + StateTax;
            }
            //var location = _unitOfWork.Location.Get(l => l.Id == id, includes: "City,County,State");

            //if (location != null)
            //{
            //    CityTax = location.City?.TaxRate ?? 0;
            //    CountyTax = location.County?.TaxRate ?? 0;
            //    StateTax = location.State?.TaxRate ?? 0;

            //    // Calculate total location tax
            //    TotalLocationTax = CityTax + CountyTax + StateTax;
            //}

            // Fetch discounts associated with the property
            PropertyDiscounts = _unitOfWork.PropertyDiscount
                .GetAll(pd => pd.PropertyId == id, includes: "Discount")
                .ToList();

            // Calculate total price for 7 nights
            double totalPriceForSevenNights = PriceForSevenNights;

            // Add fees to the total price
            totalPriceForSevenNights += PropertyFees.Sum(fee => fee.Fee.Percentage ?? 0.0);

            // Calculate total tax
            double totalTax = totalPriceForSevenNights * ((CityTax + CountyTax + StateTax) / 100);

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

            var currentDate = DateTime.Today.AddYears(-1);
            var availableDates = new List<DateTime>();

            foreach (var booking in bookings)
            {
                if (booking.IsConfirmed)
                {
                    if (currentDate < booking.Checkin.Date)
                    {
                        // If there's a gap between the current date and the check-in date of the next booking, add the available dates
                        availableDates.AddRange(GetDatesInRange(currentDate, booking.Checkin.Date.AddDays(-1)));
                    }

                    if (currentDate <= booking.Checkout.Date)
                    {
                        currentDate = booking.Checkout.Date.AddDays(1);
                    }
                }
            }

            // Add available dates after the last booking
            if (currentDate <= DateTime.Today.AddYears(1)) // Adjust the range as needed
            {
                availableDates.AddRange(GetDatesInRange(currentDate, DateTime.Today.AddYears(1))); // Adjust the range as needed
            }

            return availableDates;
        }
        
        private IEnumerable<DateTime> GetUnavailableDates(int propertyId)
        {
            var bookings = _unitOfWork.Booking.GetAll(b => b.PropertyId == propertyId)
                                               .OrderBy(b => b.Checkin)
                                               .ToList();

            var unavailableDates = new List<DateTime>();

            foreach (var booking in bookings)
            {
                if (booking.IsConfirmed)
                {
                    unavailableDates.AddRange(GetDatesInRange(booking.Checkin.Date, booking.Checkout.Date));
                }
            }

            return unavailableDates;
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

        //FROM REVIEW
        public string UserId => _userManager.GetUserId(User);
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Data Error unable to connect to database";
                return Page();
            }

            if (!(objBooking.Checkout < DateTime.Now) && !(UserId == objBooking.GuestId)) { 
                //blank - could put error code here if so desired. Additional check if user stayed at housing
            }

            /* FOR COMMIT */
            //associate Review.BookingId to Booking.Booking.Id
            objUserReview.BookingId = objBooking.Id;
            // Associate Booking.PropertyId to Property.Id
            objBooking.PropertyId = objProperty.Id;
            // Associate Booking.GuestId to User.Id
            _user = _unitOfWork.ApplicationUser.GetById(UserId);
            objBooking.GuestId = _user.Id;

            if (objUserReview.Id == 0)
            {
                objUserReview.Rating = ReviewStars.FirstOrDefault();
                //ReviewStars.GetType();
                //use drop down for now
                objUserReview.Comment = userComment.ToString();
                objUserReview.Timestamp = DateTime.Now;

                //rework this for better logic -  listers can rent
                if (User.IsInRole(SD.ListerRole))
                {
                    objUserReview.ReviewType = true;
                }
                else if (User.IsInRole(SD.RenterRole))
                {
                    objUserReview.ReviewType = false;
                }

                _unitOfWork.Commit();
            }

            //else
            //{
            //    _unitOfWork.Commit();
            //    return RedirectToPage("./Index");
            //}

            return Page();
        }
    }
}
