using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Infrastructure.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Booking")]
        public int BookingId { get; set; } // References the booking associated with the property or user being reviewed

        [ForeignKey("BookingId")]
        public Booking? Booking { get; set; }

        [Required]
        public int Rating { get; set; } // The rating out of 10 (5 stars with possible half stars) given in the review

        public string? Comment { get; set; } // An explanation of the rating given

        [Required]
        public DateTime Timestamp { get; set; } // The date and time the review was posted

        [Required]
        public bool ReviewType { get; set; } // Whether the review was left by the renter on the property or by the property owner on the renter (true = review left by renter, false = review left by owner)
    }
}
