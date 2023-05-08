using SRP.Models.Commons;
using SRP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using SRP.Models.DTOs;

namespace SRP.Interfaces
{
    public interface IUsersService
    {
        Task<IList<SRPUserDTO>> GetUnconfirmedUsersAsync();
        Task<IList<SRPUserDTO>> GetAllUsers();
        Task<bool> ConfirmUser(Guid id);        /// <summary>
                                                /// dasdasdasdas
                                                /// </summary>
                                                /// <param name="userId"></param>
                                                /// <returns>Zwraca True w przypadku zablokowania użytkownika.</returns>
        Task<bool> LockoutUserAsync(Guid id);
        Task<SRPUserDTO> Details(Guid id);

        Task<GetUserWithRolesListVm> GetUserWithRolesList(Guid id);
        /// <summary>
        /// To jest posdisdsd
        /// </summary>
        /// <param name="request">parametr wchodzący jego poisdsadasdas</param>
        /// <returns>Zwraca false jeśli operacja nie uda się w całości. True jeśli wszystko pójdzie zgodnie z planem.</returns>
        Task<bool> ModifyUserRolesAsync(ChangeRoleForUserRequest request);

        Task<bool> UnlockUser(Guid id);
        Task<List<SRPUserDTO>> GetLockedOutUsers();
        Task<SRPUserDTO> GetUserByIdAsync(Guid requestId);
        Task<PaginatedList<SRPUserDTO>> GetUsers(int pageNumber, int pageSize = 10, string query = null);

        List<SRPUserDTO> Users { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<IList<SRPUserDTO>> GetUsersInRole(string roleName);
        Task<string> GetUserClaim(Guid userId, string claimName);
        Task SetUserClaim(Guid userId, string claimName, string claimValue);
    }
}
