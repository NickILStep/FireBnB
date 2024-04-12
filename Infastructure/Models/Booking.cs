using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        //[Required]
        //[Display(Name = "Guest")]
        public string? GuestId { get; set; } // References the user who made the booking

        // Foreign key removed due to multiple cascade paths causing issues
        //[ForeignKey("GuestId")]
        //public ApplicationUser? ApplicationUser { get; set; }

        [Required]
        [Display(Name = "Property")]
        public int PropertyId { get; set; } // References the property being booked

        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }

        [Required]
        public DateTime Checkin { get; set; } // When the renter can check in to the property

        [Required]
        public DateTime Checkout { get; set; } // When the renter must check out from the property

        [Required]
        public float Tax { get; set; } // Tax to be collected on the order

        [Required]
        public float TotalPrice { get; set; } // Total price of the order

        [Required]
        public int NumGuests { get; set; } // Number of guests who will be staying at the property for the booking

        [Required]
        public bool IsConfirmed { get; set; } // Whether or not the lister has accepted the booking request
    }
}
