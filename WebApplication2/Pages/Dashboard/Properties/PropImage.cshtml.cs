using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FireBnBWeb.Pages.Dashboard.Properties
{
    public class PropImageModel : PageModel
    {
        public readonly UnitofWork _unitofWork;
        public IEnumerable<Image> ImagesList { get; set; }
		[BindProperty]
		public Image objImage { get; set; }
        public Property objproperty { get; set; }
        public bool thereprimary { get; set; }
        public int propid { get; set; }
        public int image {  get; set; }
        public Image primImage { get; set; }
        public PropImageModel(UnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            objImage = new Image();
            objproperty = new Property();
        }
        public IActionResult OnGet(int id)
        {
            propid = id;
            ImagesList = _unitofWork.Image.GetAll(p => p.PropertyId == id);
            return Page();
        }
		
		public IActionResult OnPost()
        {
			if (!ModelState.IsValid)
			{
				TempData["error"] = "Data Incomplete";
				return Page();
			}
            
            _unitofWork.Image.Add(objImage);
            _unitofWork.Commit();
			if (objImage.IsPrimary)
			{
				objproperty = _unitofWork.Property.GetById(objImage.PropertyId);
                primImage = _unitofWork.Image.GetAll().LastOrDefault();
				objproperty.ImageId = primImage.Id;
                _unitofWork.Property.Update(objproperty);
                _unitofWork.Commit();
			}
			return RedirectToPage("./PropImage", new { id = objImage.PropertyId });
		}
    }
}
