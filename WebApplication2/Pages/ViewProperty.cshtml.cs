using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FireBnBWeb.Pages
{
    public class ViewPropertyModel : PageModel
    {
        private readonly UnitofWork _unitofWork;
        public Property property;
        /*
        public ViewPropertyModel(UnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            property = new Property();
        }*/

        public void OnGet()
        {
        }
    }
}
