using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String? StatusName { get; set; } // Possible status for a property (like available, hidden, etc.)
    }
}
