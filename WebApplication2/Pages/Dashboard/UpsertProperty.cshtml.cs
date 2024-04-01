using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FireBnBWeb.Pages.Dashboard
{
    public class UpsertPropertyModel : PageModel
    {
        private readonly UnitofWork _unitofwork;
        private readonly UserManager<ApplicationUser> _userManager; // Inject UserManager
        private ApplicationUser _user;

        public Property objproperty { get; set; }
        public PropertyAmenity objpropamenity { get; set; }
        public IEnumerable<Amenity> amenity { get; set; }
        public List<int> SelectedAmenities { get; set; }
        public PropertyBedConfiguration objpropbedconfig { get; set; }
        public IEnumerable<BedConfiguration> BedList { get; set; }
        //public PropertyDiscount objpropdiscount { get; set; }
        //public IEnumerable<Discount> DiscountList { get; set; }
        //public PropertyFee objpropfee { get; set; }
        //public IEnumerable<Fee> FeeList { get; set; }
        //public PropertyNightlyPrice objpropprice { get; set; }
        //public IEnumerable<PropertyNightlyPrice> PropPriceList { get; set; }
        
        public PropertyType objproptype
        { get; set; }
        public IEnumerable<PropertyType> TypeList { get; set; }

        public UpsertPropertyModel(UnitofWork unitofwork, UserManager<ApplicationUser> userManager)
        {
            _unitofwork = unitofwork;
            _userManager = userManager;
            objproperty = new Property();
            objpropamenity = new PropertyAmenity();
            amenity = new List<Amenity>();
            objpropbedconfig = new PropertyBedConfiguration();
            //objpropdiscount = new PropertyDiscount();
            //objpropfee = new PropertyFee();
            objproptype = new PropertyType();
            //PropFeeList = new List<PropertyFee>();
            //PropPriceList = new List<PropertyNightlyPrice>();
            TypeList = new List<PropertyType>();
            BedList = new List<BedConfiguration>();
            //PropDiscountList = new List<PropertyDiscount>();
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            _user = await _userManager.GetUserAsync(User);
            if (id != 0)
            {
                objproperty = _unitofwork.Property.GetById(id);
                // PropDiscountList = _unitofwork.PropertyDiscount.GetAll(p => p.PropertyId == id, includes: "Discount");
                //PropBedList = _unitofwork.PropertyBedConfiguration.GetAll(p => p.PropertyId == id, includes: "BedConfiguragtion");
                //PropFeeList = _unitofwork.PropertyFee.GetAll(p => p.PropertyId == id, includes: "Fee");
                //PropPriceList = _unitofwork.PropertyNightlyPrice.GetAll(p => p.PropertyId == id, includes: "NightlyPrice");

            }
            amenity = _unitofwork.Amenity.GetAll();
            TypeList = _unitofwork.PropertyType.GetAll();
            BedList = _unitofwork.BedConfiguration.GetAll();

            if (objproperty == null)
            {
                return NotFound();
            }
            return Page();
        }
        public string UserId => _userManager.GetUserId(User);
        public DateTime dateTime => DateTime.Now;
        public IActionResult OnPost()
        {
            if (objproperty.Id == 0)
            {
                _unitofwork.Property.Add(objproperty);

            }
            

            _unitofwork.Commit();
            if (objproperty.Id == 0)
            {
                return RedirectToPage("./UserDashboard");
            }
            return Page();
        }
    }
}
