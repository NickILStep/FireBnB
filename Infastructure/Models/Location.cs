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

        [Required]
        [Display(Name = "City")]
        public int CityId { get; set; } // References the city

        [ForeignKey("CityId")]
        public City? City { get; set; }

        [Required]
        [Display(Name = "County")]
        public int CountyId { get; set; } // References the county

        [ForeignKey("CountyId")]
        public County? County { get; set; }

        [Required]
        [Display(Name = "State")]
        public int StateId { get; set; } // References the state

        [ForeignKey("StateId")]
        public State? State { get; set; }

        [Required]
        public string? Address { get; set; } // The street address for the property

        [Required]
        public string? Zipcode { get; set; } // The postal code for the property
    }
}
