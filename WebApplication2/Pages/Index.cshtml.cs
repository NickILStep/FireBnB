using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FireBnBWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UnitofWork _unitofwork;
        public IEnumerable<Property> objProperties;

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? CheckIn { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? CheckOut { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? GuestNumber { get; set; }


        public IndexModel(ILogger<IndexModel> logger, UnitofWork unitofwork)
        {
            _unitofwork = unitofwork;
            _logger = logger;
            objProperties = new List<Property>();
        }

        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(SearchQuery) || CheckIn.HasValue || CheckOut.HasValue || GuestNumber.HasValue)
            {
                // Perform search based on provided parameters
                objProperties = _unitofwork.Property.SearchProperties(SearchQuery, CheckIn, CheckOut, GuestNumber);
            }
            else
            {
                // If no search parameters provided, get all properties
                objProperties = _unitofwork.Property.GetAll();
            }

            return Page();
        }
    }
}