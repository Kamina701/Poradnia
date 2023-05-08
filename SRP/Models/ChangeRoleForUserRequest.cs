using System.Collections.Generic;
using System;

namespace SRP.Models
{
    public class ChangeRoleForUserRequest
    {
        public Guid Id { get; set; }
        public List<AddRoleToUser> UserWithRoles { get; set; }
    }
}
