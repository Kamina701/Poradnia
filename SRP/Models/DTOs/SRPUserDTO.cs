using System.Collections.Generic;
using System;
using SRP.Models.Enums;

namespace SRP.Models.DTOs
{
    public class SRPUserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsLockedOut { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public IList<string> Role { get; set; }
        public Status Status { get; set; }
    }
}
