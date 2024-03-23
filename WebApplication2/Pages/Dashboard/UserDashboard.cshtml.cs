using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public IEnumerable<Booking> bookingList;
        public IEnumerable<Property> propertyList;
        public IEnumerable<Location> locationList;

        public UserDashboardModel(UnitofWork unitofWork, UserManager<ApplicationUser> userManager)
        {
            _unitofWork = unitofWork;
            _userManager = userManager;
            bookingList = new List<Booking>();
            propertyList = new List<Property>();
            locationList = new List<Location>();

        }
        public string FullName { get; private set; }
        public async Task<IActionResult> OnGetAsync()
        {
            // Get the currently logged-in user
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Set the full name
            FullName = $"{user.FirstName} {user.LastName}";

            return Page();
        }
    }
}
