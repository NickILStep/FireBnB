using DataAccess;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FireBnBWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UnitofWork _unitofwork;
        public IEnumerable<Property> objProperties;
        public IEnumerable<Property> Properties { get; private set; }
        //public IEnumerable<Location> locations;

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


        public IndexModel(ILogger<IndexModel> logger, UnitofWork unitofwork)
        {
            _unitofwork = unitofwork;
            _logger = logger;
            objProperties = new List<Property>();
            AmenityOptions = _unitofwork.Amenity.GetAll().Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.AmenityName
            }).ToList();
        }

        public IActionResult OnGet()
        {
            // Retrieve all properties along with their related locations, cities, counties, and states
            objProperties = _unitofwork.GetAllWithLocationsCitiesCountiesStates();

            // Retrieve properties along with their locations, cities, counties, and states
            Properties = _unitofwork.Property.GetAllWithLocationsCitiesCountiesStates();

            if (!string.IsNullOrEmpty(SearchQuery) || CheckIn.HasValue || CheckOut.HasValue || GuestNumber.HasValue || CostPerNight.HasValue || SelectedAmenities?.Any() == true || BedroomCount.HasValue || BathroomCount.HasValue)
            {
                // Perform search based on provided parameters SearchProperties(string searchQuery, DateTime? checkIn, DateTime? checkOut, int? guestNumber, decimal? costPerNight, List<int> amenityIds, List<int> SelectedAmenities, int? bedroomCount, int? bathroomCount);
                objProperties = _unitofwork.Property.SearchProperties(SearchQuery, CheckIn, CheckOut, GuestNumber, CostPerNight, SelectedAmenities, BedroomCount, BathroomCount);
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