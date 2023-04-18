using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Infrastructure
{
    public class GetUserWithRolesListVm
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public List<AddRoleToUser> UserWithRoles { get; set; }
    }
}
