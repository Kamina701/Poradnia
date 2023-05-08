using System.Threading.Tasks;
using System;

namespace SRP.interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        bool IsAdmin { get; }
        string UserName { get; }
        string mailAddress { get; }
        string Role { get; }

        Task<string> GetClaim(string name);
        Task SetClaim(string name, string value);
    }
}
