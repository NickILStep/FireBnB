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
    public class Location
    {
        [Key]
        public int Id { get; set; }
        //Navigation Properties
        [ForeignKey("CityId")]
        public City? City { get; set; }

        [ForeignKey("CountyId")]
        public County? County { get; set; }

        [ForeignKey("StateId")]
        public State? State { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? Zipcode { get; set; }
    }
}
