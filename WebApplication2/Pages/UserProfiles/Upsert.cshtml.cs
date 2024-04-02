using DataAccess;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FireBnBWeb.Pages.UserProfiles
{
    public class UpsertModel : PageModel
    {
        private readonly UnitofWork _unitofWork;
        private readonly UserManager<ApplicationUser> _userManager; // Inject UserManager
        private ApplicationUser _user;
        private object _unitOfWork;

        // Constructor to initialize UnitOfWork and UserManager
        public UpsertModel(UnitofWork unitofWork, UserManager<ApplicationUser> userManager)
        {
            _unitofWork = unitofWork;
            _userManager = userManager;
        }

        // Handle HTTP GET requests
        public async Task<IActionResult> OnGetAsync()
        {
            // Get the currently logged-in user
            _user = await _userManager.GetUserAsync(User);

            // If the user is not found, return a 404 error
            if (_user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // If the user is found, return the page
            return Page();
        }

        // POST
        public IActionResult OnPost(int? id)
        {
            // Update
            _user = _unitofWork.ApplicationUser.Get(x => x.Id == _user.Id);

            //update the existing product
            _unitofWork.ApplicationUser.Update(_user);

            //Save Changes to DB
            _unitofWork.Commit();

            return RedirectToPage("./Index");
        }

        // Properties to access user data in the Razor Page
        public string UserId => _userManager.GetUserId(User);
        public string FullName => $"{_user.FirstName} {_user.LastName}";
        public string FirstName => _user.FirstName;
        public string LastName => _user.LastName;
        public string Email => _user.Email;
        public string? PhoneNumber => _user.PhoneNumber;
        public string? DisplayName => _user.DisplayName;
        public DateTime Birthdate => _user.Birthdate;
        public DateTime SignupDate => _user.SignupDate;
        public string? ProfilePictureUrl => _user.ProfilePictureUrl;
    }
}
