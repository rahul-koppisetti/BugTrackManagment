using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BugTrackManagment.Models
{
    public class AppUser:IdentityUser 
    {
        [Required]     
        public Dept Department { get; set; }

        public static implicit operator AppUser(ClaimsPrincipal v)
        {
            throw new NotImplementedException();
        }
    }
}
