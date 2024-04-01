using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class PropertyNightlyPrice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Property")]
        public int PropertyId { get; set; } // References the property

        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }

        //[Required]
        //[Display(Name = "PriceRange")]
        //public int PriceRangeId { get; set; } // References the price range (when this price will be in effect)

        //[ForeignKey("PriceRangeId")]
        //public PriceRange? PriceRange { get; set; }

        [Required]
        public DateTime StartDate { get; set; } // The start date for when a certain price will apply to a property

        [Required]
        public DateTime EndDate { get; set; } // The end date for when a certain price will apply to a property

        [Required]
        public float Rate { get; set; } // The price to rent the property
    }
}
