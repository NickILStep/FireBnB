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
        public Image objImage { get; set; }
        public bool thereprimary { get; set; }
        public PropImageModel(UnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            objImage = new Image();
        }
        public IActionResult OnGet(int? id)
        {
            ImagesList = _unitofWork.Image.GetAll(p => p.PropertyId == id);
            return Page();
        }
    }
}
