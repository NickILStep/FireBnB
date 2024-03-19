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
        //[Key]
        //public int Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        //[Required]
        //public string Email { get; set; }

        //[DisplayName("Phone Number")]
        //public string? PhoneNumber { get; set; }

        //[Required]
        //public string? Password { get; set; }

        [DisplayName("Display Name")]
        public string? DisplayName { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public DateTime SignupDate { get; set; }
    }
}
