using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace Lab2_AysncInn.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }


        [NotMapped]
        public IList<string>? Roles { get; set; }
    }
}

