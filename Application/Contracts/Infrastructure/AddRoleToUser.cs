using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Infrastructure
{
    public class AddRoleToUser
    {
        public AddRoleToUser()
        {

        }
        public AddRoleToUser(string role, bool add)
        {
            Role = role;
            Add = add;
        }
        public string Role { get; set; }
        public bool Add { get; set; }
    }
}
