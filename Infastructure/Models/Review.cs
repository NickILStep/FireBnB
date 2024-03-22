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
        public int BookingId { get; set; }

        [ForeignKey("BookingId")]
        public Booking? Booking { get; set; }

        [Required]
        public int Rating { get; set; }

        public string? Comment { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public int ReviewType { get; set; }
    }
}
