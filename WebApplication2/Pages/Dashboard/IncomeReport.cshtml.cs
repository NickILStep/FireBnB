using DataAccess;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using IronPdf;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace FireBnBWeb.Pages.Dashboard
{
    public class IncomeReportModel : PageModel
    {
        private readonly UnitofWork _unitofWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public IEnumerable<Booking> BookingList { get; set; }
        public IEnumerable<Property> PropertyList { get; set; }
        public decimal TotalIncome { get; set; }
        public Dictionary<string, decimal> WeeklyIncome { get; set; }
        public Dictionary<string, decimal> MonthlyIncome { get; set; }
        public decimal LastMonthIncome { get; set; }
        public decimal LastWeekIncome { get; set; }

        public decimal CurrentMonthIncome { get; set; }
        public decimal CurrentWeekIncome { get; set; }

        public IncomeReportModel(UnitofWork unitofWork, UserManager<ApplicationUser> userManager) { 
            _unitofWork = unitofWork;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync(int? propertyId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var propertyQuery = _unitofWork.Property.GetAll()
                .Where(p => p.ListerId == currentUser.Id);

            PropertyList = await Task.Run(() => propertyQuery.ToList());

            var bookingQuery = _unitofWork.Booking.GetAll()
                .Where(b => PropertyList.Select(p => p.Id).Contains(b.PropertyId));

            BookingList = await Task.Run(() => bookingQuery.ToList());

            TotalIncome = (decimal)BookingList.Sum(b => b.TotalPrice);

            if (propertyId.HasValue)
            {
                // Filter bookings for the selected property
                BookingList = _unitofWork.Booking.GetAll()
                    .Where(b => b.PropertyId == propertyId.Value)
                    .ToList();
            }

            CalculateWeeklyIncome();
            CalculateMonthlyIncome();
            // Calculate total income from last month
            CalculateLastMonthIncome();
            CalculateLastWeekIncome();
            CalculateCurrentMonthIncome();
            CalculateCurrentWeekIncome();


            return Page();
        }
        public async Task<IActionResult> OnGetGeneratePdfAsync(int? propertyId)
        {
            await OnGetAsync(propertyId);

            // Generate HTML content for the PDF
            var htmlContent = $@"
                <h1>Income Report</h1>
                <p>Total Income: {TotalIncome:C}</p>
                <p>Last Month's Income: {LastMonthIncome:C}</p>
                <p>Last Week's Income: {LastWeekIncome:C}</p>
                <p>Current Month's Income: {CurrentMonthIncome:C}</p>
                <p>Current Week's Income: {CurrentWeekIncome:C}</p>";

            // Initialize IronPdf renderer
            var renderer = new ChromePdfRenderer();

            // Render HTML content to PDF
            var pdf = renderer.RenderHtmlAsPdf(htmlContent);

            // Save the PDF to a file or return it as a file download
            return File(pdf.BinaryData, "application/pdf", "IncomeReport.pdf");
        }

        private void CalculateWeeklyIncome()
        {
            WeeklyIncome = new Dictionary<string, decimal>();

            foreach (var booking in BookingList)
            {
                var totalPrice = booking.TotalPrice;
                var checkinDate = booking.Checkin;
                var checkoutDate = booking.Checkout;

                var weeks = Math.Ceiling((checkoutDate - checkinDate).TotalDays / 7);
                var weeklyIncomeForBooking = totalPrice / (float)weeks;
                var propertyId = booking.PropertyId.ToString();

                // Retrieve the property associated with the booking
                var property = PropertyList.FirstOrDefault(p => p.Id == booking.PropertyId);
                if (property != null)
                {
                    // Retrieve the tax rates for the city, county, and state associated with the property
                    var cityTaxRate = property.City?.TaxRate ?? 0;
                    var countyTaxRate = property.County?.TaxRate ?? 0;
                    var stateTaxRate = property.State?.TaxRate ?? 0;

                    // Apply tax rates to calculate the net income
                    var totalTaxRate = cityTaxRate + countyTaxRate + stateTaxRate;
                    var netIncomeForBooking = totalPrice * (1 - totalTaxRate);

                    if (!WeeklyIncome.ContainsKey(propertyId))
                    {
                        WeeklyIncome[propertyId] = 0;
                    }
                    WeeklyIncome[propertyId] += (decimal)(netIncomeForBooking / weeks);
                }
            }
        }

        private void CalculateMonthlyIncome()
        {
            MonthlyIncome = new Dictionary<string, decimal>();

            foreach (var booking in BookingList)
            {
                var totalPrice = booking.TotalPrice;
                var checkinDate = booking.Checkin;
                var checkoutDate = booking.Checkout;

                var daysInMonth = DateTime.DaysInMonth(checkinDate.Year, checkinDate.Month);
                var monthlyIncomeForBooking = totalPrice * (daysInMonth / (float)(checkoutDate - checkinDate).TotalDays);
                var propertyId = booking.PropertyId.ToString();

                // Retrieve the property associated with the booking
                var property = PropertyList.FirstOrDefault(p => p.Id == booking.PropertyId);
                if (property != null)
                {
                    // Retrieve the tax rates for the city, county, and state associated with the property
                    var cityTaxRate = property.City?.TaxRate ?? 0;
                    var countyTaxRate = property.County?.TaxRate ?? 0;
                    var stateTaxRate = property.State?.TaxRate ?? 0;

                    // Apply tax rates to calculate the net income
                    var totalTaxRate = cityTaxRate + countyTaxRate + stateTaxRate;
                    var netIncomeForBooking = totalPrice * (1 - totalTaxRate);

                    if (!MonthlyIncome.ContainsKey(propertyId))
                    {
                        MonthlyIncome[propertyId] = 0;
                    }
                    MonthlyIncome[propertyId] += (decimal)(netIncomeForBooking * (daysInMonth / (float)(checkoutDate - checkinDate).TotalDays));
                }
            }
        }

        private void CalculateLastMonthIncome()
        {
            // Calculate the first day of the previous month
            var firstDayOfLastMonth = DateTime.Today.AddMonths(-1).Date.AddDays(1 - DateTime.Today.Day);

            // Calculate the last day of the previous month
            var lastDayOfLastMonth = DateTime.Today.AddDays(-DateTime.Today.Day);

            // Filter bookings made within the previous month
            var lastMonthBookings = BookingList.Where(b => b.Checkin >= firstDayOfLastMonth && b.Checkin <= lastDayOfLastMonth);

            // Calculate total income from last month
            LastMonthIncome = lastMonthBookings.Sum(b => (decimal)b.TotalPrice);
        }
        private void CalculateLastWeekIncome()
        {
            // Calculate the first day of the previous week (last week)
            var firstDayOfLastWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 6);

            // Calculate the last day of the previous week (last week)
            var lastDayOfLastWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);

            // Filter bookings made within the previous week
            var lastWeekBookings = BookingList.Where(b => b.Checkin >= firstDayOfLastWeek && b.Checkin <= lastDayOfLastWeek);

            // Calculate total income from last week
            LastWeekIncome = lastWeekBookings.Sum(b => (decimal)b.TotalPrice);
        }

        private void CalculateCurrentMonthIncome()
        {
            // Calculate the first day of the current month
            var firstDayOfCurrentMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            // Calculate the last day of the current month
            var lastDayOfCurrentMonth = firstDayOfCurrentMonth.AddMonths(1).AddDays(-1);

            // Filter bookings made within the current month
            var currentMonthBookings = BookingList.Where(b => b.Checkin >= firstDayOfCurrentMonth && b.Checkin <= lastDayOfCurrentMonth);

            // Calculate total income for the current month
            CurrentMonthIncome = currentMonthBookings.Sum(b => (decimal)b.TotalPrice);
        }

        private void CalculateCurrentWeekIncome()
        {
            // Calculate the first day of the current week (Sunday)
            var firstDayOfCurrentWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);

            // Calculate the last day of the current week (Saturday)
            var lastDayOfCurrentWeek = firstDayOfCurrentWeek.AddDays(6);

            // Filter bookings made within the current week
            var currentWeekBookings = BookingList.Where(b => b.Checkin >= firstDayOfCurrentWeek && b.Checkin <= lastDayOfCurrentWeek);

            // Calculate total income for the current week
            CurrentWeekIncome = currentWeekBookings.Sum(b => (decimal)b.TotalPrice);
        }
    }
}
