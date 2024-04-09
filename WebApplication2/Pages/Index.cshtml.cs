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
                Value = c.Id.ToString(), // Assuming Id is the ID of the city
                Text = c.CityName
            }).ToList();

            Counties = _unitofwork.County.GetAll().Select(c => new SelectListItem
            {
                Value = c.Id.ToString(), // Assuming Id is the ID of the county
                Text = c.CountyName
            }).ToList();

            States = _unitofwork.State.GetAll().Select(s => new SelectListItem
            {
                Value = s.Id.ToString(), // Assuming Id is the ID of the state
                Text = s.StateName
            }).ToList();

            //// If a city is selected, filter properties by city
            //if (SelectedCityId != null)
            //{
            //    Properties = Properties.Where(property => property.CityId == SelectedCityId);
            //}

            //// If a county is selected, filter properties by county
            //if (SelectedCountyId != null)
            //{
            //    Properties = Properties.Where(property => property.CountyId == SelectedCountyId);
            //}

            //// If a state is selected, filter properties by state
            //if (SelectedStateId != null)
            //{
            //    Properties = Properties.Where(property => property.StateId == SelectedStateId);
            //}

            //// If max guest count is specified, filter properties by max guest count
            //if (GuestNumber != null)
            //{
            //    Properties = Properties.Where(property => property.GuestMax >= GuestNumber);
            //}
            //if (BedroomCount != null)
            //{
            //    Properties = Properties.Where(property => property.BedroomNum >= BedroomCount);
            //}
            //if (BathroomCount != null)
            //{
            //    Properties = Properties.Where(property => property.BathroomNum >= BathroomCount);
            //}

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
    }
}