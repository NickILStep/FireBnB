using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FireBnBWeb.Pages.Dashboard.Properties
{
    public class PayPerNightModel : PageModel
    {
        public readonly UnitofWork _unitofWork;
		[BindProperty]
		public PropertyNightlyPrice objNightPrice { get; set; }
		public IEnumerable<PropertyNightlyPrice> objNightlyPriceList { get; set; }
        
		public int propertyid;

        public PayPerNightModel(UnitofWork unitofWork) 
        {
            _unitofWork = unitofWork;
            objNightPrice = new PropertyNightlyPrice();
        }
        public IActionResult OnGet(int id)
        {
            propertyid = id;
            objNightlyPriceList = _unitofWork.PropertyNightlyPrice.GetAll(p => p.PropertyId == id);
            
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Data Incomplete";
                return Page();
            }
            
            
            _unitofWork.PropertyNightlyPrice.Add(objNightPrice);
            _unitofWork.Commit();
			return RedirectToPage("./PayPerNight/", new { id = objNightPrice.PropertyId });
		}
    }
}
