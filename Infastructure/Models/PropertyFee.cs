using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class PropertyFee
    {
		[Key]
		public int Id { get; set; }
		[ForeignKey("PropertyId")]
        public Property? Property { get; set; }

        [ForeignKey("FeeId")]
        public Fee? Fee { get; set; }
    }
}
