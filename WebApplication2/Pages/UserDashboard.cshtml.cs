using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

//At the momeny nor working. Getting an error with dependency injection with UnitofWork
namespace FireBnBWeb.Pages
{
    public class UserDashboardModel : PageModel
    {
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
