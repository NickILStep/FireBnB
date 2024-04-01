using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FireBnBWeb.Pages.BookingsPages
{
    /*
     Renter side:
        Here, the user will choose the following:
            How many days
            How many guest
            Which days do they want? 
        Display on the side of the screen is a live breakdown of the cost
            Price per night added
            Location tax
            Any fees
            Any deals the location has. 
        The display will be policies
            Cancellation policy we have.
            Being respectful policy. 
        Then, a note will be sent that the lister can cancel the stay, and the user will get a full refund. 
        There is a button labeled ‘Request to book.’ 
    Lister side:
        Here, the lister can see the requested booking:
            How many days
            How many guest
            Which days do they want? 
            Which locations
        Display on the side of the screen the cost
            Price per night added
            Location tax
            Any fees
            Any deals the location has. 
        The display user reviews
        The lister will have two buttons: approve and deny.  

     */
    public class BookingRequestModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
