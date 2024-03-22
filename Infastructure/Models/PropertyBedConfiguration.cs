using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class PropertyBedConfiguration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Property")]
        public int PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }

        [Required]
        [Display(Name = "BedConfiguration")]
        public int BedConfigurationId { get; set; }

        [ForeignKey("BedConfigurationId")]
        public BedConfiguration? BedConfiguration { get; set; }
    }
}
