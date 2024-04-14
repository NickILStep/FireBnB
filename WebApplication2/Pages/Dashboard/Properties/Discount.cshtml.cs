using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FireBnBWeb.Pages.Dashboard.Properties
{
    public class DiscountModel : PageModel
    {
		public UnitofWork _unitofWork;
		[BindProperty]
		public PropertyDiscount objDiscount {  get; set; }
		public IEnumerable<PropertyDiscount> objDiscountList { get; set; }
		public int? propertyid;

		public DiscountModel(UnitofWork unitofWork)
		{
			_unitofWork = unitofWork;
			objDiscount = new PropertyDiscount();
		}
        public IActionResult OnGet(int? id)
        {
			propertyid = id;
			return Page();
		}
        public IActionResult OnPost()
        {
			if (!ModelState.IsValid)
			{
				TempData["error"] = "Data Incomplete";
				return Page();
			}
			_unitofWork.PropertyDiscount.Add(objDiscount);
			_unitofWork.Commit();
			return Page();
		}
    }
}
