using System;
using Microsoft.AspNetCore.Identity;

namespace GMS.Data.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
