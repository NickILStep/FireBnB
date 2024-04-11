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

        [BindProperty(SupportsGet = true)]
        public int? SelectedCityId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedCountyId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedStateId { get; set; }

        // Add properties to hold the list of cities, counties, and states
        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> Counties { get; set; }
        public List<SelectListItem> States { get; set; }

        public ICollection<Image> Images { get; set; }
        public Dictionary<int, string> PropertyImages { get; set; }


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

            // Populate dropdown lists for cities, counties, and states
            Cities = _unitofwork.City.GetAll().Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CityName
            }).ToList();

            Counties = _unitofwork.County.GetAll().Select(c => new SelectListItem
            {
                Value = c.Id.ToString(), 
                Text = c.CountyName
            }).ToList();

            States = _unitofwork.State.GetAll().Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.StateName
            }).ToList();


           
            // If search criteria are provided, perform the search
            if (!string.IsNullOrEmpty(SearchQuery) || CheckIn != null || CheckOut != null || GuestNumber != null || CostPerNight != null || SelectedAmenities != null || BedroomCount != null || BathroomCount != null || SelectedCityId != null || SelectedCountyId != null || SelectedStateId != null)
            {
                // Call the SearchProperties method from your repository
                Properties = _unitofwork.Property.SearchProperties(SearchQuery, CheckIn, CheckOut, GuestNumber, CostPerNight, SelectedAmenities, BedroomCount, BathroomCount, SelectedCityId, SelectedCountyId, SelectedStateId);
            }
            else
            {
                // If no search criteria are provided, do not fetch properties again
                // Instead, use the already fetched Properties list
                // This list was populated with location information earlier
                // and does not need to be fetched again
            }

            return Page();
        }
        public string GetPrimaryImageUrl(int imageId)
        {
            var primaryImage = _unitofwork.Image.GetById(imageId);
            return primaryImage != null ? primaryImage.Url : null;
        }
        public float? GetNightlyPrice(int propertyId)
        {
            var currentDate = DateTime.Now;
            var nightlyPrice = _unitofwork.PropertyNightlyPrice.GetAll()
                .FirstOrDefault(p => p.PropertyId == propertyId && p.StartDate <= currentDate && p.EndDate >= currentDate);

            return nightlyPrice?.Rate;
        }


    }
}