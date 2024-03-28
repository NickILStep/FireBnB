using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class BedConfiguration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; } // Number of beds in the room

        [Required]
        public string? Configuration { get; set; } // Style (and sometimes layout) of beds
    }
}
