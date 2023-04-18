using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Identity.Models
{
    public class SRPUser : IdentityUser<Guid>
    {
        public SRPUser() : base()
        {
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<IdentityUserClaim<Guid>> Claims { get; set; }

        public ICollection<SRPUserRole> UserRoles { get; set; }


    }
}