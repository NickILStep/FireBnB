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
        public string? ListerId { get; set; }

        [ForeignKey("ListerId")]
        public ApplicationUser? ApplicationUser { get; set; }

        [Required]
        [Display(Name = "PropertyType")]
        public int PropertyTypeId { get; set; }

        [ForeignKey("PropertyTypeId")]
        public PropertyType? PropertyType { get; set; }

        [Required]
        [Display(Name = "Location")]
        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location? Location { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status? Status { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        public Boolean GuestSharing { get; set; }

        [Required]
        public int GuestMax { get; set; }

        [Required]
        public int BedroomNum { get; set; }

        [Required]
        public int BathroomNum { get; set; }
    }
}
