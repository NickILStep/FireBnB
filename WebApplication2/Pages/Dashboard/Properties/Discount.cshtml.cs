using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FireBnBWeb.Pages.Dashboard.Properties
{
    public class DiscountModel : PageModel
    {
		public UnitofWork _unitofWork;
		[BindProperty]
		public PropertyDiscount objDiscount {  get; set; }
		public List<SelectListItem> discountoption { get; set; }
		public IEnumerable<PropertyDiscount> objDiscountList { get; set; }
		[BindProperty]
		public List<int> Discountchoice { get; set; }
		public int propertyid;

		public DiscountModel(UnitofWork unitofWork)
		{
			_unitofWork = unitofWork;
			objDiscount = new PropertyDiscount();
		}
        public IActionResult OnGet(int id)
        {
			propertyid = id;
			objDiscountList = _unitofWork.PropertyDiscount.GetAll(p=> p.PropertyId == propertyid, includes: "Discount");
			discountoption = _unitofWork.Discount.GetAll().Select(a => new SelectListItem
			{
				Value = a.Id.ToString(),
				Text = "Value: " + a.Value.ToString() + " Code: " + a.Code.ToString() + " Expiration: " + a.Expiration.ToString()
			}).ToList();
			return Page();
		}
        public IActionResult OnPost()
        {
			if (!ModelState.IsValid)
			{
				TempData["error"] = "Data Incomplete";
				return Page();
			}
			objDiscount.DiscountId = Discountchoice.FirstOrDefault();
			_unitofWork.PropertyDiscount.Add(objDiscount);
			_unitofWork.Commit();
			return Page();
		}
    }
}
