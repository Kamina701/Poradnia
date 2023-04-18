using Microsoft.AspNetCore.Identity;
using System;

namespace Identity.Models
{
    public class SRPUserRole : IdentityUserRole<Guid>
    {
        public SRPUser User { get; set; }
        public SRPRole Role { get; set; }
    }
}