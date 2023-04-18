using System;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        bool IsAdmin { get; }
        string UserName { get; }
        string Role { get; }

        Task<string> GetClaim(string name);
        Task SetClaim(string name, string value);
    }
}
