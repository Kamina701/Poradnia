using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Infrastructure
{
    public class ChangeRoleForUserRequest
    {
        public Guid Id { get; set; }
        public List<AddRoleToUser> UserWithRoles { get; set; }
    }
}
