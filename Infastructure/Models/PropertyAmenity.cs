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
    public class PropertyAmenity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Property")]
        public int PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }

        [Required]
        [Display(Name = "Amenity")]
        public int AmenityId { get; set; }

        [ForeignKey("AmenityId")]
        public Amenity? Amenity { get; set; }
    }
}
