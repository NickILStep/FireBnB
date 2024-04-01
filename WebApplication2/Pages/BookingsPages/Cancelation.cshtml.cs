using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FireBnBWeb.Pages.BookingsPages
{
    /*
     * We are offering a time-over percentage for fees. It would start 7 days before the booking, and the percentage would increase as each day passed.
            On booking confirmation and the dashboard, renters can cancel their booking.
        Have it happen on the same screen and double-check if they want to cancel with the cancellation fee.  
        Same thing for Lister, but with no cancellation fee. 
     */
    public class CancelationModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
