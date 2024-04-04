using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utility;

namespace FireBnBWeb.Pages.Dashboard
{
    public class UpsertPropertyModel : PageModel
    {
        private readonly UnitofWork _unitofwork;
        private readonly UserManager<ApplicationUser> _userManager; // Inject UserManager
        private ApplicationUser _user;
        [BindProperty]
        public Property objproperty { get; set; }
        public PropertyAmenity objpropamenity { get; set; }
        public PropertyBedConfiguration objpropbedconfig { get; set; }
        public IEnumerable<BedConfiguration> BedList { get; set; }
        
        [BindProperty]
        public List<int> StateList { get; set; }
        [BindProperty]
        public List<int> CityList { get; set; }
        [BindProperty]
        public List<int> CountyList { get; set; }
        [BindProperty]
        public List<int> AmenityList { get; set; }

        [BindProperty]
        public List<int> TypeList { get; set; }
        public List<SelectListItem> StateOptions { get; set; }
        public List<SelectListItem> CityOptions { get; set; }
        public List<SelectListItem> CountyOptions { get; set; }

        public List<SelectListItem> AmenityOptions { get; set; }
        public List<SelectListItem> PropTypeOptions { get; set; }
        
        public Property PropOptions {  get; set; }
        public ApplicationUser AppUser { get; set; }

        public PropertyType objproptype
        { get; set; }
       

        public UpsertPropertyModel(UnitofWork unitofwork, UserManager<ApplicationUser> userManager)
        {
            _unitofwork = unitofwork;
            _userManager = userManager;
            objproperty = new Property();
            objpropamenity = new PropertyAmenity();
            objpropbedconfig = new PropertyBedConfiguration();
            objproptype = new PropertyType();
            
            BedList = new List<BedConfiguration>();
            
            StateOptions = _unitofwork.State.GetAll().Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.StateName
            }).ToList();
            CityOptions = _unitofwork.City.GetAll().Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.CityName
            }).ToList();
            CountyOptions = _unitofwork.County.GetAll().Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.CountyName
            }).ToList();
            AmenityOptions = _unitofwork.Amenity.GetAll().Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.AmenityName
            }).ToList();
            PropTypeOptions = _unitofwork.PropertyType.GetAll().Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Title
            }).ToList();
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            _user = await _userManager.GetUserAsync(User);
            if (id != 0)
            {
                objproperty = _unitofwork.Property.GetById(id);

            }
            

            if (objproperty == null)
            {
                return NotFound();
            }
            return Page();
        }
        public string UserId => _userManager.GetUserId(User);

        public DateTime dateTime => DateTime.Now;
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Data Incomplete";
                return Page();
            }

            if (objproperty.Id == 0)
            {
                objproperty.StatusId = 1;
                objproperty.StateId = StateList.FirstOrDefault();
                objproperty.CityId = CityList.FirstOrDefault();
                objproperty.CountyId = CountyList.FirstOrDefault();
                objproperty.PropertyTypeId = TypeList.FirstOrDefault();
                objproperty.StateId = StateList.FirstOrDefault();
                _unitofwork.Property.Add(objproperty);
                _unitofwork.Commit();
                PropOptions = _unitofwork.Property.GetAll().LastOrDefault();

                foreach (var amenity in AmenityList)
                {
                    objpropamenity = new PropertyAmenity();
                    objpropamenity.PropertyId = PropOptions.Id;
                    objpropamenity.AmenityId = amenity;
                    _unitofwork.PropertyAmenity.Add(objpropamenity);
                    AppUser = _unitofwork.ApplicationUser.GetById(UserId);
                    _unitofwork.Commit();
                    
                }
                if (User.IsInRole(SD.ListerRole)) { }
                else
                {
                    await _userManager.AddToRoleAsync(AppUser, SD.ListerRole);
                }
            }
            

            _unitofwork.Commit();
            if (objproperty.Id == 0)
            {
                return RedirectToPage("UserDashboard");
            }
            return Page();
        }
    }
}
