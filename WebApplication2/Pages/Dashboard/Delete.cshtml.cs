using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Utility;

namespace FireBnBWeb.Pages.Dashboard
{
    public class DeleteModel : PageModel
    {
        private readonly UnitofWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public Property objproperty{ get; set; }
        public PropertyAmenity objpropamenity { get; set; }
        public PropertyBedConfiguration objpropbedconfig { get; set; }
        public PropertyFee objPropFee { get; set; }
        public PropertyNightlyPrice objnightprice { get; set; }
        public PropertyDiscount objdiscount { get; set; }

        public IEnumerable<PropertyAmenity> propamenitieslist { get; set; }
        public IEnumerable<PropertyBedConfiguration> propbedconfigurationsList { get; set; }
        public IEnumerable<PropertyFee> propertyFeesList { get; set; }
        public IEnumerable<PropertyNightlyPrice> propertyNightlyPricesList { get; set; }
        public IEnumerable<PropertyDiscount> propertyDiscountList { get; set; } 
        public IEnumerable<Property> checkproperty {  get; set; }
        public string UserId => _userManager.GetUserId(User);
        public ApplicationUser AppUser { get; set; }
        public int currentproperty;
        public DeleteModel(UnitofWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            objproperty = new Property();
            propamenitieslist = new List<PropertyAmenity>();
            propbedconfigurationsList = new List<PropertyBedConfiguration>();
            propertyNightlyPricesList = new List<PropertyNightlyPrice>();
            propertyFeesList = new List<PropertyFee>();
            propertyDiscountList = new List<PropertyDiscount>();

        }
        public IActionResult OnGet(int? id)
        {
            if (id != 0)
            {
                objproperty = _unitOfWork.Property.GetById(id);
                
                
    }
            if (objproperty == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Data Error unable to connect to database";
                return Page();
            }

            else
            {
                currentproperty = objproperty.Id;
                _unitOfWork.Property.Delete(objproperty);
                _unitOfWork.Commit();
                
                TempData["success"] = "Category deleted Successfully";
                
            }
            _unitOfWork.Commit();

            checkproperty = _unitOfWork.Property.GetAll(p => p.ListerId == UserId);
            if(checkproperty == null)
            {
                await _userManager.RemoveFromRoleAsync(AppUser, SD.ListerRole);
            }
            return RedirectToPage("./UserDashboard");
        }
    }
}
