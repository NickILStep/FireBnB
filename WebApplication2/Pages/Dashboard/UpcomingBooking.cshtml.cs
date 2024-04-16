using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FireBnBWeb.Pages.Dashboard
{
    //This shoudl be only for renters and listers
    //Display all upcoming booking the user has in a table format
    //Dates, locations, guest count, and how much
    public class UpcomingBookingModel : PageModel
    {
        //bring in the database
        private readonly UnitofWork _unitofWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public IEnumerable<Booking> bookingList;
        public IEnumerable<Property> propertyList;

        public UpcomingBookingModel(UnitofWork unitofWork, UserManager<ApplicationUser> userManager)
        {
            _unitofWork = unitofWork;
            _userManager = userManager;
            bookingList = new List<Booking>();
            propertyList = new List<Property>();

        }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Fetch bookings only for the current user
            bookingList = _unitofWork.Booking.GetAll(b => b.GuestId == user.Id).ToList();

            // Retrieve property IDs from bookings
            var propertyIds = bookingList.Select(b => b.PropertyId).Distinct().ToList();

            // Fetch properties including location details
            propertyList = _unitofWork.Property.GetAllWithLocationsCitiesCountiesStates()
                .Where(p => propertyIds.Contains(p.Id))
                .ToList();

            return Page();
        }
    }
}
