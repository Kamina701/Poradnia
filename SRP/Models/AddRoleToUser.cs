using System.Data;

namespace SRP.Models
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
