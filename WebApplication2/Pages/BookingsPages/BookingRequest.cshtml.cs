using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace FireBnBWeb.Pages.BookingsPages
{
    /*
     Renter side:
        Here, the user will choose the following:
            How many days
            How many guest
            Which days do they want? 
        Display on the side of the screen is a live breakdown of the cost
            Price per night added
            Location tax
            Any fees
            Any deals the location has. 
        The display will be policies
            Cancellation policy we have.
            Being respectful policy. 
        Then, a note will be sent that the lister can cancel the stay, and the user will get a full refund. 
        There is a button labeled �Request to book.� 
    Lister side:
        Here, the lister can see the requested booking:
            How many days
            How many guest
            Which days do they want? 
            Which locations
        Display on the side of the screen the cost
            Price per night added
            Location tax
            Any fees
            Any deals the location has. 
        The display user reviews
        The lister will have two buttons: approve and deny.  

     */
    public class BookingRequestModel : PageModel
    {
        private readonly UnitofWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationUser _user;

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

        [BindProperty]
        public Booking objBooking { get; private set; }
        public string adf { get; private set; }
        public string adl { get; private set; }
        public float currentPrice { get; private set; }

        [BindProperty]
        public int guestNum { get; set; }
        [BindProperty]
        public DateTime startDate { get; set; }
        [BindProperty]
        public DateTime endDate { get; set; }
        

        public BookingRequestModel(UnitofWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            objBooking = new Booking();
            _userManager = userManager;
        }


        public IActionResult OnGet(int id)
        {
            // Fetch the property along with its location and related entities
            Property = _unitOfWork.Property.GetAllWithLocationsCitiesCountiesStates().FirstOrDefault(p => p.Id == id);
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
            DateTime FirstDate = AvailableDates.First();
            DateTime LastDate = AvailableDates.Last();
            // The whole statement is to craft the specific string for the date picker controls.
            // The controls need the 0 (i.e. 04, not 4) or it doesn't work
            if(FirstDate.Month < 10 && FirstDate.Day < 10)
            {
                adf = FirstDate.Year + "-0" + FirstDate.Month + "-0" + FirstDate.Day;
            }
            else if (FirstDate.Month < 10 && FirstDate.Day >= 10)
            {
                adf = FirstDate.Year + "-0" + FirstDate.Month + "-" + FirstDate.Day;
            }
            else if (FirstDate.Month >= 10 && FirstDate.Day < 10)
            {
                adf = FirstDate.Year + "-" + FirstDate.Month + "-0" + FirstDate.Day;
            }
            else
            {
                adf = FirstDate.Year + "-" + FirstDate.Month + "-" + FirstDate.Day;
            }

            if (LastDate.Month < 10 && LastDate.Day < 10)
            {
                adl = LastDate.Year + "-0" + LastDate.Month + "-0" + LastDate.Day;
            }
            else if (LastDate.Month < 10 && LastDate.Day >= 10)
            {
                adl = LastDate.Year + "-0" + LastDate.Month + "-" + LastDate.Day;
            }
            else if (LastDate.Month >= 10 && LastDate.Day < 10)
            {
                adl = LastDate.Year + "-" + LastDate.Month + "-0" + LastDate.Day;
            }
            else
            {
                adl = LastDate.Year + "-" + LastDate.Month + "-" + LastDate.Day;
            }

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

            var currentDate = DateTime.Now.Date;
            //var currentDate = bookings.First().Checkin.Date;
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

        public string UserId => _userManager.GetUserId(User);
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Data Incomplete";
                return Page();
            }

            //objBooking.PropertyId = Property.Id;
            //objBooking.GuestId = UserId;
            //objBooking.NumGuests = guestNum;
            //objBooking.Checkin = startDate;
            //objBooking.Checkout = endDate;

            // Fetch location details for the property
            //var location = _unitOfWork.Property.Get(p => p.Id == id, includes: "City,County,State");
            // Fetch nightly prices associated with the property
            //NightlyPrices = _unitOfWork.PropertyNightlyPrice
            //    .GetAll(pnp => pnp.PropertyId == id).ToList;

            //CityTax = location.City?.TaxRate ?? 0;
            //CountyTax = location.County?.TaxRate ?? 0;
            //StateTax = location.State?.TaxRate ?? 0;
            // Calculate total location tax
            TotalLocationTax = CityTax + CountyTax + StateTax;
            objBooking.Tax = TotalLocationTax;
            //objBooking.TotalPrice = obj TotalLocationTax - Discount; 
            objBooking.TotalPrice = 100 + TotalLocationTax;

            /*
             * GuestID
             * NumGuests
             * CheckIn
             * CheckOut
             * Tax (??)
             * TotalPrice
             * */

            return Page();
        }
    }
}
