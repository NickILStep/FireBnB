using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        //Navigation Properties
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
