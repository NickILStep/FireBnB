using DataAccess;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using Utility;

namespace FireBnBWeb.Pages.Dashboard
{
    /*
    - Allow renters to create reviews for properties they have previously stayed at. You should be able to click on the property and add a review. 
        ~ use pop up box for leaving review or new razor page?
    - Allowing Lister to create a review for a renter who has stayed at their property previously. 

    - Creating a review for a user or a property
    - Renters can only review the properties they have been to, and the lister can review users who have been to their property. 
    - Have the user review be displayed in the profile. 
    - Have property displayed in view property, and manage a property. 

    Need to work roles
    - if role = renter show div x
    - if role = lister show div y

    Me todo:
    1. View properties the renter has previously stayed at
    2. next step

     */
    public class ReviewModel : PageModel
    {
        //Injection
        private readonly ILogger<IndexModel> _logger;
        private readonly UnitofWork _unitofWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationUser _user;


        public IEnumerable<Property> objProperties;
        public IEnumerable<Property> Properties { get; private set; }
        public IEnumerable<Booking> bookingList;
        public Review objUserReview { get; set; }
        public Booking objBooking { get; set; }
        public Property objProperty { get; set; }


        //Get & Set For Property
        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? CheckIn { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? CheckOut { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? GuestNumber { get; set; }
        [BindProperty(SupportsGet = true)]
        public decimal? CostPerNight { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<int> SelectedAmenities { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? BedroomCount { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? BathroomCount { get; set; }

        public List<SelectListItem> AmenityOptions { get; set; }
        public City City { get; set; } // Navigation property for the associated city
        public State State { get; set; }
        public County County { get; set; }



        //Get & Set For Review
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

        public ReviewModel(ILogger<IndexModel> logger, UnitofWork unitofWork, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _unitofWork = unitofWork;
            _userManager = userManager;
            objProperties = new List<Property>();
            Properties = new List<Property>();
            bookingList = new List<Booking>();
            objUserReview = new Review();
            objBooking = new Booking();
            objProperty = new Property();
            AmenityOptions = _unitofWork.Amenity.GetAll().Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.AmenityName
            }).ToList();
        }

        //Methods

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Fetch bookings only for the current user
            var bookings = _unitofWork.Booking.GetAll(b => b.GuestId == user.Id).ToList();

            // Retrieve property IDs from bookings
            var propertyIds = bookings.Select(b => b.PropertyId).Distinct().ToList();

            // Fetch properties including location details
            objProperties = _unitofWork.Property.GetAllWithLocationsCitiesCountiesStates()
                .Where(p => propertyIds.Contains(p.Id))
                .ToList();
            
            // Retrieve all properties along with their related locations, cities, counties, and states
            objProperties = _unitofWork.GetAllWithLocationsCitiesCountiesStates();
            

            if (!string.IsNullOrEmpty(SearchQuery) || CheckIn.HasValue || CheckOut.HasValue || GuestNumber.HasValue || CostPerNight.HasValue || SelectedAmenities?.Any() == true || BedroomCount.HasValue || BathroomCount.HasValue)
            {
                // Perform search based on provided parameters SearchProperties(string searchQuery, DateTime? checkIn, DateTime? checkOut, int? guestNumber, decimal? costPerNight, List<int> amenityIds, List<int> SelectedAmenities, int? bedroomCount, int? bathroomCount);
                objProperties = _unitofWork.Property.SearchProperties(SearchQuery, CheckIn, CheckOut, GuestNumber, CostPerNight, SelectedAmenities, BedroomCount, BathroomCount);
            }
            else
            {
                // If no search parameters provided, get all properties
                objProperties = _unitofWork.Property.GetAll();
            }

            return Page();
        }

        public string UserId => _userManager.GetUserId(User);
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Data Error unable to connect to database";
                return Page();
            }

            /* For Commit */
            //associate Review.BookingId to Booking.Booking.Id
            objUserReview.BookingId = objBooking.Id;
            // Associate Booking.PropertyId to Property.Id
            objBooking.PropertyId = objProperty.Id;
            // Associate Booking.GuestId to User.Id
            _user = _unitofWork.ApplicationUser.GetById(UserId);
            objBooking.GuestId = _user.Id;

            if (objUserReview.Id == 0)
            {
                objUserReview.Rating = ReviewStars.FirstOrDefault();
                //use drop down for now
                objUserReview.Comment = userComment.ToString();
                objUserReview.Timestamp = DateTime.Now;
                if (User.IsInRole(SD.ListerRole))
                {
                    objUserReview.ReviewType = true;
                }
                else if (User.IsInRole(SD.RenterRole))
                {
                    objUserReview.ReviewType = false;
                }

                _unitofWork.Commit();
            }

            else
            {
                _unitofWork.Commit();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
