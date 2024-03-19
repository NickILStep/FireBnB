using DataAccess;
using Infrastructure.Models;
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
        public IEnumerable<Booking> bookingList;
        public IEnumerable<Property> propertyList;

        public UpcomingBookingModel(UnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            bookingList = new List<Booking>();
            propertyList = new List<Property>();

        }
        public IActionResult OnGet()
        {
            bookingList = _unitofWork.Booking.GetAll(); 
            propertyList = _unitofWork.Property.GetAll();
            return Page();
        }
    }
}
