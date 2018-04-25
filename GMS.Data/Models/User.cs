using System;
using Microsoft.AspNetCore.Identity;

namespace GMS.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
