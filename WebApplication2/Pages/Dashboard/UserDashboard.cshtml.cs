using DataAccess;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Utility;

namespace FireBnBWeb.Pages.Dashboard
{
    //This just displays the dashbaords
    //Only two roles should be able to access this
    //Renter, tells them to add a home
    //Lister, show people who book their home
    public class UserDashboardModel : PageModel
    {
        //bring in the database
        private readonly UnitofWork _unitofWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationUser _user;
        public IEnumerable<Booking> bookingList;
        public IEnumerable<Property> propertyList;
        //public IEnumerable<Location> locationList;
        public IDictionary<int, List<Booking>> propertyBookings;
        public List<Status> Statuses { get; } = new List<Status>
    {
        new Status { StatusName = "Available" },
        new Status { StatusName = "Hidden" },
        new Status { StatusName = "Partially Available" },
        new Status { StatusName = "Suspended" },
        new Status { StatusName = "Owner Use" }
    };

        public UserDashboardModel(UnitofWork unitofWork, UserManager<ApplicationUser> userManager)
        {
            _unitofWork = unitofWork;
            _userManager = userManager;
            bookingList = new List<Booking>();
            propertyList = new List<Property>();
            //locationList = new List<Location>();
            propertyBookings = new Dictionary<int, List<Booking>>();

        }
        public string FullName { get; private set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var userManager = HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>();
            // Get the currently logged-in user
            var user = await _userManager.GetUserAsync(User);

            // Fetch primary images for each property and store their URLs
            propertyList = _unitofWork.Property.GetAllWithLocationsCitiesCountiesStates();

            bookingList = _unitofWork.Booking.GetAll( predicate: b => b.GuestId == user.Id, includes: "Property.County").ToList();

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            // Check if the user is in the "Renter" role
            if (User.IsInRole(SD.RenterRole))
            {
                // User is a renter, fetch their bookings
                bookingList = _unitofWork.Booking.GetAll(predicate: b => b.GuestId == user.Id, includes: "Property.County").ToList();
            }
            else
            {
                // User is a lister, fetch their properties
                propertyList = _unitofWork.Property.GetAll(predicate: p => p.ListerId == user.Id);

                // Fetch property bookings for the lister
                foreach (var property in propertyList)
                {
                    var bookingsForProperty = _unitofWork.Booking.GetAll(predicate: b => b.PropertyId == property.Id);
                    propertyBookings[property.Id] = bookingsForProperty.ToList();
                }
            }

            // Set the full name
            FullName = $"{user.FirstName} {user.LastName}";

            return Page();
        }
        // Function to get the guest's full name based on their ID
        public string GetGuestName(string guestId)
        {
            var guest = _userManager.FindByIdAsync(guestId).Result; // Find the user by ID
            return $"{guest.FirstName} {guest.LastName}";
        }
        // Add this method to your UserDashboardModel class
        public Property GetPropertyForBooking(Booking booking)
        {
            // Fetch the associated property for the booking
            return _unitofWork.Property.Get(p => p.Id == booking.PropertyId);
        }
        // Properties to access user data in the Razor Page
        public string UserId => _userManager.GetUserId(User);
        public string FirstName => _user.FirstName;
        public string LastName => _user.LastName;

    }
}
