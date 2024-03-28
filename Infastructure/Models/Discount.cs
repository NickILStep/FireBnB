using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public float Value { get; set; } // How much money the discount takes off of the final total

        [Required]
        public String? Code { get; set; } // String of letters and numbers that must be typed in on checkout to redeem discount

        [Required]
        public DateTime Expiration { get; set; } // Final day the discount is able to be applied
    }
}
