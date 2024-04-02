using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Infrastructure.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Lister")]
        public string? ListerId { get; set; } // References the ApplicationUser who added and owns the property

        [ForeignKey("ListerId")]
        public ApplicationUser? ApplicationUser { get; set; }

        [Required]
        [Display(Name = "PropertyType")]
        public int PropertyTypeId { get; set; } // References which type of property this is

        [ForeignKey("PropertyTypeId")]
        public PropertyType? PropertyType { get; set; }

        //[Required]
        //[Display(Name = "Location")]
        //public int LocationId { get; set; } // References where the property is located (and taxes to apply to the property)

        //[ForeignKey("LocationId")]
        //public Location? Location { get; set; }

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

        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; } // References the property's current listing status

        [ForeignKey("StatusId")]
        public Status? Status { get; set; }

        [Required]
        public string? Title { get; set; } // What the property is called

        [Required]
        public string? Description { get; set; } // Additional information about the property

        [Required]
        public DateTime LastUpdated { get; set; } // The date and time of when the owner commited the most recent changes to the property listing

        [Required]
        public Boolean GuestSharing { get; set; } // Whether the property will be shared with other unassociated renters

        [Required]
        public int GuestMax { get; set; } // The maximum allowable number of guests to be included in a booking

        [Required]
        public int BedroomNum { get; set; } // Number of bedrooms in the property

        [Required]
        public int BathroomNum { get; set; } // Number of bathrooms in the property

        [Required]
        public int CancellationDays { get; set; } // Number of days prior to a booking when cancellation will be free

        [Required]
        public float BasePrice { get; set; } // Base price to rent this property for a night. PropertyNightlyPrice table will override this value

        [Required]
        public int ImageId { get; set; } // The id of the property's primary image
    }
}
