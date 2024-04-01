using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FireBnBWeb.Pages.Dashboard
{
    /*
     Allow renters to create reviews for properties they have previously stayed at. You should be able to click on the property and add a review. 
    Allowing Lister to create a review for a renter who has stayed at their property previously. 

    Creating a review for a user or a property
    Renters can only review the properties they have been to, and the lister can review users who have been to their property. 
    Have the user review be displayed in the profile. 
    Have property displayed in view property, and manage a property. 

     */
    public class ReviewModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
