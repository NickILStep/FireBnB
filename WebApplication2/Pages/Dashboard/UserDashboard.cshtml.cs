using DataAccess;
using Infrastructure.Models;
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
        public IEnumerable<Booking> bookingList;
        public IEnumerable<Property> propertyList;
        public IEnumerable<Location> locationList;

        public UserDashboardModel(UnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            bookingList = new List<Booking>();
            propertyList = new List<Property>();
            locationList = new List<Location>();

        }
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
