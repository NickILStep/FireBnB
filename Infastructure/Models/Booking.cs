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
        public string? GuestId { get; set; }

        //[ForeignKey("GuestId")]
        //public ApplicationUser? ApplicationUser { get; set; }

        [Required]
        [Display(Name = "Property")]
        public int PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }

        // [ForeignKey("DiscountId")]
        //public Discount? Discount { get; set; }

        [Required]
        public DateTime Checkin { get; set; }

        [Required]
        public DateTime Checkout { get; set; }

        //[Required]
        //public int ServiceFee { get; set; }

        //[Required]
        //public int CleaningFee { get; set; }

        [Required]
        public float Tax { get; set; }

        [Required]
        public float TotalPrice { get; set; }
    }
}
