using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess;

namespace FireBnBWeb.Pages.UserProfiles
{
    public class IndexModel : PageModel
    {
        //private readonly IUnitofWork _unitofWork;
        ////private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ApplicationUser Users;
        ////for user data, use userManager or ApplicationUser for user info. Will need to inject into backend.

        //public IndexModel(UnitofWork unitofWork, ApplicationUser user)
        //{
        //    //_userManager = userManager;
        //    _unitofWork = unitofWork;
        //    Users = user;
        //}

        //public IActionResult OnGet()
        //{
        //    //Users = _unitofWork.ApplicationUser.Get(Users.Id);
        //    return Page();
        //}
    }
}
