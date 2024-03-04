using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class PropertyType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }
    }
}
