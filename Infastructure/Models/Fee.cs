using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Fee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Type { get; set; }

        [Required]
        public double? Price { get; set; }
    }
}
