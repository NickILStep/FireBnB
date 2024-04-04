using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FireBnBWeb.Pages.Dashboard
{
    public class DeleteModel : PageModel
    {
        private readonly UnitofWork _unitOfWork;

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
        public DeleteModel(UnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                propamenitieslist = _unitOfWork.PropertyAmenity.GetAll(p=>p.PropertyId == id);
                propbedconfigurationsList = _unitOfWork.PropertyBedConfiguration.GetAll(p => p.PropertyId == id);
                propertyDiscountList = _unitOfWork.PropertyDiscount.GetAll(p=>p.PropertyId == id);
                propertyNightlyPricesList = _unitOfWork.PropertyNightlyPrice.GetAll(p=>p.PropertyId == id);
                propertyFeesList = _unitOfWork.PropertyFee.GetAll(p=>p.PropertyId == id);

            }
            if (objproperty == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Data Error unable to connect to database";
                return Page();
            }

            else
            {
                _unitOfWork.Property.Delete(objproperty);
                foreach(var amenity in propamenitieslist)
                {
                    objpropamenity = new PropertyAmenity();
                    objpropamenity = amenity;
                    _unitOfWork.PropertyAmenity.Delete(objpropamenity);
                    _unitOfWork.Commit();
                }
                foreach(var bed in propbedconfigurationsList)
                {
                    objpropbedconfig = new PropertyBedConfiguration();
                    objpropbedconfig = bed;
                    _unitOfWork.PropertyBedConfiguration.Delete(objpropbedconfig);
                    _unitOfWork.Commit();
                }
                foreach(var fee in propertyFeesList)
                {
                    objPropFee = new PropertyFee();
                    objPropFee = fee;
                    _unitOfWork.PropertyFee.Delete(objPropFee);
                    _unitOfWork.Commit();
                }
                foreach(var night in propertyNightlyPricesList)
                {
                    objnightprice = new PropertyNightlyPrice();
                    objnightprice = night;
                    _unitOfWork.PropertyNightlyPrice.Delete(objnightprice);
                    _unitOfWork.Commit();
                }
                foreach(var discount in  propertyDiscountList)
                {
                    objdiscount = new PropertyDiscount();
                    objdiscount = discount;
                    _unitOfWork.PropertyDiscount.Delete(objdiscount);
                    _unitOfWork.Commit();
                }
                TempData["success"] = "Category deleted Successfully";
            }
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}
