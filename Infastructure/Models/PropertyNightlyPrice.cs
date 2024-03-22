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
        public int PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }

        [Required]
        [Display(Name = "PriceRange")]
        public int PriceRangeId { get; set; }

        [ForeignKey("PriceRangeId")]
        public PriceRange? PriceRange { get; set; }

        [Required]
        public float Rate { get; set; }
    }
}
