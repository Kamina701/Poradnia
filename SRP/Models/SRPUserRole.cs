using Microsoft.AspNetCore.Identity;
using System;

namespace SRP.Models
{
    public class SRPUserRole : IdentityUserRole<Guid>
    {
        public SRPUser User { get; set; }
        public SRPRole Role { get; set; }
    }
}
