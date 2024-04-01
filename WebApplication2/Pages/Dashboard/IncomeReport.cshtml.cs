using DataAccess;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FireBnBWeb.Pages.Dashboard
{
    public class IncomeReportModel : PageModel
    {
        private readonly UnitofWork _unitofWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public IEnumerable<Booking> BookingList { get; set; }
        public IEnumerable<Property> PropertyList { get; set; }
        public decimal TotalIncome { get; set; }

        public IncomeReportModel(UnitofWork unitofWork, UserManager<ApplicationUser> userManager)
        {
            _unitofWork = unitofWork;
            _userManager = userManager;

        }
        public async Task<IActionResult> OnGetAsync()
        {
            // Get the currently logged-in user (owner)
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Query database to get all properties owned by the current user
            var propertyQuery = _unitofWork.Property.GetAll()
                .Where(p => p.ListerId == currentUser.Id);

            PropertyList = await Task.Run(() => propertyQuery.ToList());

            // Query database to get all bookings associated with the properties owned by the current user
            var bookingQuery = _unitofWork.Booking.GetAll()
                .Where(b => PropertyList.Select(p => p.Id).Contains(b.PropertyId));

            BookingList = await Task.Run(() => bookingQuery.ToList());

            // Calculate total income
            TotalIncome = (decimal)BookingList.Sum(b => b.TotalPrice);

            return Page();
        }
    }
}
