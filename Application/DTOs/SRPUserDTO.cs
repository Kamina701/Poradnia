using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
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

    public enum Status
    {
        Aktywny,
        Zablokowany,
        Niepotwierdzony
    }
    public class SRPUserAccessCopyDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool CopyFrom { get; set; }

    }
}
