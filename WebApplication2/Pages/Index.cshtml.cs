using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FireBnBWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UnitofWork _unitofwork;
        public IEnumerable<Property> objProperties;

        public IndexModel(ILogger<IndexModel> logger, UnitofWork unitofwork)
        {
            _unitofwork = unitofwork;
            _logger = logger;
            objProperties = new List<Property>();
        }

        public IActionResult OnGet()
        {
            objProperties = _unitofwork.Property.GetAll();
            return Page();

        }
    }
}