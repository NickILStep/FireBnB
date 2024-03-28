using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FireBnBWeb.Pages.Dashboard
{
    public class UpsertPropertyModel : PageModel
    {
        private readonly UnitofWork _unitofwork;

        public Property objproperty { get; set; }
        public PropertyAmenity objpropamenity { get; set; }
        public IEnumerable<PropertyAmenity> PropAmenityList { get; set; }

        public PropertyBedConfiguration objpropbedconfig { get; set; }
        public IEnumerable<PropertyBedConfiguration> PropBedList { get; set; }
        public PropertyDiscount objpropdiscount { get; set; }
        public IEnumerable<PropertyDiscount> PropDiscountList { get; set; }
        public PropertyFee objpropfee { get; set; }
        public IEnumerable<PropertyFee> PropFeeList { get; set; }
        public PropertyNightlyPrice objpropprice { get; set; }
        public IEnumerable<PropertyNightlyPrice> PropPriceList { get; set; }
        public PropertyType objproptype { get; set; }
        public IEnumerable<PropertyType> PropTypeList { get; set; }

        public UpsertPropertyModel(UnitofWork unitofwork)
        {
            _unitofwork = unitofwork;
            objproperty = new Property();
            objpropamenity = new PropertyAmenity();
            objpropbedconfig = new PropertyBedConfiguration();
            objpropdiscount = new PropertyDiscount();
            objpropfee = new PropertyFee();
            objproptype = new PropertyType();
            PropAmenityList = new List<PropertyAmenity>();
            PropFeeList = new List<PropertyFee>();
            PropPriceList = new List<PropertyNightlyPrice>();
            PropTypeList = new List<PropertyType>();
            PropBedList = new List<PropertyBedConfiguration>();
            PropDiscountList = new List<PropertyDiscount>();
        }

        public IActionResult OnGet(int? id)
        {
            if (id != 0)
            {
                objproperty = _unitofwork.Property.GetById(id);
                PropAmenityList = _unitofwork.PropertyAmenity.GetAll(p=>p.PropertyId == id, includes:"Amenity");
                PropDiscountList = _unitofwork.PropertyDiscount.GetAll(p => p.PropertyId == id, includes: "Discount");
                PropBedList = _unitofwork.PropertyBedConfiguration.GetAll(p => p.PropertyId == id, includes: "BedConfiguragtion");
                PropFeeList = _unitofwork.PropertyFee.GetAll(p => p.PropertyId == id, includes: "Fee");
                PropPriceList = _unitofwork.PropertyNightlyPrice.GetAll(p => p.PropertyId == id, includes: "NightlyPrice");
                
            }
            if (objproperty == null)
            {
                return NotFound();
            }
            return Page();
        }
        
    }
}
