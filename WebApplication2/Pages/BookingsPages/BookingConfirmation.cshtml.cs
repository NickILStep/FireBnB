using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FireBnBWeb.Pages.BookingsPages
{
    /*
     * Renter side:
        The renter will receive a notification that their booking has been approved. To access this page, click the notification or go to the dashboard. 
        This page is a lot like the request form in terms of display.
            Location details
            User details of stay
            The price they paid. 
        At the bottom of this page will be two buttons. 
            Edit
            Cancel: 
        Lister:
            The lister can still view details of the booking even after it was approved. 
            This page is a lot like the request form in terms of display.
                Location details
                User details of stay
                The price they paid. 
            At the bottom of this page will be 1 button. 
                Cancel
     */
    public class BookingConfirmationModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
