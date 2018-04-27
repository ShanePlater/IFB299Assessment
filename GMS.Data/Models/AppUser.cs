using System;
using Microsoft.AspNetCore.Identity;

namespace GMS.Data.Models
{
    /// <summary>
    /// Application implementation of the IdentityUser class provided by the ASP Net Identity framework for user authentication. Type Guid set as primary key
    /// </summary>
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
