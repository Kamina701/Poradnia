using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SRP.Models
{
    public class SRPRole : IdentityRole<Guid>
    {
        public ICollection<SRPUserRole> UserRoles { get; set; }
    }
}
