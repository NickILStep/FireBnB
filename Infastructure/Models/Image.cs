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
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Property")]
        public int PropertyId { get; set; } // References the property that the image is associated with

        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }

        [Required]
        public String? Url { get; set; } // A url to the uploaded image

        [Required]
        public bool IsPrimary { get; set; } // Indicates if the image will be the first to display for the property (only one image per property should be set to true)
    }
}
