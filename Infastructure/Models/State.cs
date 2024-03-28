using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class State
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String? StateName { get; set; } // Name of state

        [Required]
        public float TaxRate { get; set; } // State's tax rate
    }
}
