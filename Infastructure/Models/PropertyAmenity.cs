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
        [Key]
        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }

        [Key]
        [ForeignKey("AmenityId")]
        public Amenity? Amenity { get; set; }
    }
}
