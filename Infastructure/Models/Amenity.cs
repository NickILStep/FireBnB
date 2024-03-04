using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Amenity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String? AmenityName { get; set; }
    }
}
