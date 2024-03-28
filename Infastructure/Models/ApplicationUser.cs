using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; } // User's first name

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; } // User's last name

        [DisplayName("Display Name")]
        public string? DisplayName { get; set; } // User's chosen username

        [Required]
        public DateTime Birthdate { get; set; } // User's date of birth

        [Required]
        public DateTime SignupDate { get; set; } // Date the user created their account

        [Required]
        public string? ProfilePictureUrl { get; set; } // Url to the user's uploaded profile picture
    }
}
