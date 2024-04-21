using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FireBnBWeb.Pages.Dashboard.Properties
{
    public class FeeModel : PageModel
    {
        public readonly UnitofWork _unitofWork;
        [BindProperty]
        public PropertyFee objFee { get; set; }
		public IEnumerable<PropertyFee> feeList { get; set; }
		public int propertyid;
        public List<SelectListItem> feeOptions { get; set; }
        [BindProperty]
        public List<int> feechoice {  get; set; }
        public FeeModel(UnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            objFee = new PropertyFee();
        }
        public IActionResult OnGet(int id)
        {
			propertyid = id;
			feeList = _unitofWork.PropertyFee.GetAll(p=> p.PropertyId == id, includes:"Fee");
            feeOptions = _unitofWork.Fee.GetAll().Select(a=> new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Type + " Percentage: " + a.Percentage.ToString(),
                
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
            objFee.FeeId = feechoice.FirstOrDefault();
            _unitofWork.PropertyFee.Add(objFee);
            _unitofWork.Commit();
			return RedirectToPage("./Fee", new { id = objFee.PropertyId });
		}
    }
}
