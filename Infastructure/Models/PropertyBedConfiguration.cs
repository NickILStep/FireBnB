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
		[Key]
        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }

        [Key]
        [ForeignKey("BedConfigurationId")]
        public BedConfiguration? BedConfiguration { get; set; }
    }
}
