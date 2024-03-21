using DataAccess;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FireBnBWeb.Pages.Listings
{
    //This should only be for listers
    public class IndexModel : PageModel
    {
        private readonly UnitofWork _unitofWork;

        public IEnumerable<Property> PropertyList { get; set; }
        public Dictionary<int, Image> PrimaryImages { get; set; }
        public IndexModel(UnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            PropertyList = new List<Property>();
            PrimaryImages = new Dictionary<int, Image>();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Get lister's properties from the repository
            var listerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            PropertyList = await _unitofWork.Property.GetAllAsync(p => p.ApplicationUser.Id == listerId);
            //image stuff here
            // Fetch primary images for each property
            foreach (var property in PropertyList)
            {
                // Get the primary image for the property
                var primaryImage = await _unitofWork.Image.GetAsync(img => img.Property.Id == property.Id && img.IsPrimary);
                if (primaryImage != null)
                {
                    PrimaryImages.Add(property.Id, primaryImage);
                }
            }
            return Page();
        }
    }
}
