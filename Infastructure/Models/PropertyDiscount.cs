using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class PropertyDiscount
    {
		[Key]
		public int Id { get; set; }
		[ForeignKey("PropertyId")]
        public Property? Property { get; set; }

        [ForeignKey("DiscountId")]
        public Discount? Discount { get; set; }
    }
}
