using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FireBnBWeb.Pages.Dashboard.Properties
{
    public class PayPerNightModel : PageModel
    {
        public readonly UnitofWork _unitofWork;
        public PropertyNightlyPrice objNightPrice;
        public IEnumerable<PropertyNightlyPrice> objNightlyPriceList;
        public int? propertyid;

        public PayPerNightModel(UnitofWork unitofWork) 
        {
            _unitofWork = unitofWork;
            objNightPrice = new PropertyNightlyPrice();
        }
        public IActionResult OnGet(int? id)
        {
            propertyid = id;
            objNightlyPriceList = _unitofWork.PropertyNightlyPrice.GetAll(p => p.PropertyId == id);
            
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Data Incomplete";
                return Page();
            }
            _unitofWork.PropertyNightlyPrice.Add(objNightPrice);

            return Page();
        }
    }
}
