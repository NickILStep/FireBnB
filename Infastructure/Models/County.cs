using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class County
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String? CountyName { get; set; } // Name of county

        [Required]
        public float TaxRate { get; set; } // County's tax rate
    }
}
