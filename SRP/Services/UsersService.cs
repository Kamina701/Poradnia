﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SRP.interfaces;
using SRP.Interfaces;
using SRP.Models;
using SRP.Models.Commons;
using SRP.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SRP.Services
{
    public class UsersService : IUsersService
    {
        private UserManager<SRPUser> _userManager;
        private RoleManager<SRPRole> _roleManager;
        private readonly SrpDbContext _identityContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly SignInManager<SRPUser> _signInManager;
        private readonly IMapper _mapper;
        public UsersService(UserManager<SRPUser> userManager, IMapper mapper, RoleManager<SRPRole> roleManager, SrpDbContext identityContext, ICurrentUserService currentUserService, SignInManager<SRPUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _identityContext = identityContext;
            _currentUserService = currentUserService;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public List<SRPUserDTO> Users => _mapper.Map<List<SRPUserDTO>>(_userManager.Users.ToList());

        public async Task<bool> ConfirmUser(Guid id)
        {
            var userToConfirm = await _userManager.FindByIdAsync(id.ToString());
            var RemoveUnconfirmedRole = await _userManager.RemoveFromRoleAsync(userToConfirm, "Unconfirmed");
            if (!RemoveUnconfirmedRole.Succeeded)
            {
                return false;
            }
            if (userToConfirm.EmailConfirmed == false)
                userToConfirm.EmailConfirmed = true;
            var updateRole = await _userManager.UpdateAsync(userToConfirm);
            if (!updateRole.Succeeded)
            {
                return false;
            }
            if (!userToConfirm.EmailConfirmed)
                return false;
            return true;
        }

        public async Task<bool> ChangeRoleAsync(Guid id, string roleName, bool removeRole)
        {
            var GetNewRole = await _userManager.FindByIdAsync(userId: id.ToString());
            if (!removeRole)
                await _userManager.AddToRoleAsync(GetNewRole, roleName);
            else
                await _userManager.RemoveFromRoleAsync(GetNewRole, roleName);

            var UpdateRole = await _userManager.UpdateAsync(GetNewRole);
            if (!UpdateRole.Succeeded)
                return false;
            return true;

        }
        public async Task<SRPUserDTO> Details(Guid id)
        {
            var user = await _userManager.FindByIdAsync(userId: id.ToString());
            IList<string> role = await _userManager.GetRolesAsync(user);
            if (!role.Any())
                role.Add("Użytkownik");
            var rq = _mapper.Map<SRPUserDTO>(user);
            rq = _mapper.Map<SRPUser, SRPUserDTO>(user, opt => { opt.AfterMap((src, dest) => dest.Role = role); });
            return rq;
        }

        public async Task<IList<SRPUserDTO>> GetAllUsers()
        {
            var users =_identityContext.Users.AsQueryable();
            var rq = _mapper.Map<IList<SRPUserDTO>>(await users.ToListAsync());
            return rq;
        }

        public async Task<List<SRPUserDTO>> GetLockedOutUsers()
        {
            var lockedOutUsers = _userManager.Users.Where(u => u.LockoutEnd != null);
            return _mapper.Map<List<SRPUserDTO>>(await lockedOutUsers.ToListAsync());
        }

        public async Task<IList<SRPUserDTO>> GetUnconfirmedUsersAsync()
        {
            return _mapper.Map<IList<SRPUserDTO>>(await _userManager.GetUsersInRoleAsync("unconfirmed"));
        }

        public async Task<SRPUserDTO> GetUserByIdAsync(Guid requestId)
        {
            return _mapper.Map<SRPUserDTO>(await _userManager.FindByIdAsync(requestId.ToString()));
        }

        public async Task<string> GetUserClaim(Guid userId, string claimName)
        {
            var user = await _userManager.FindByIdAsync(userId: userId.ToString());
            var claims = await _userManager.GetClaimsAsync(user);
            return claims.FirstOrDefault(x => x.Type == claimName)?.Value;
        }

        public async Task<PaginatedList<SRPUserDTO>> GetUsers(int pageNumber, int pageSize = 10, string query = null)
        {
            var users = _identityContext.Users.Include(x => x.Claims).AsQueryable();
            if (!string.IsNullOrEmpty(query))
            {
                var normalizedQuery = query.ToUpper();
                users = users.Where(u => u.FirstName.ToUpper().Contains(normalizedQuery)
                                         || u.LastName.ToUpper().Contains(normalizedQuery)
                                         || u.NormalizedUserName.ToUpper().Contains(normalizedQuery));
            }
            var count = users.Count();
            var pagedUsers = users.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return PaginatedList<SRPUserDTO>.Create(_mapper.Map<List<SRPUserDTO>>(pagedUsers), count, pageNumber, pageSize);
        }

        public async Task<IList<SRPUserDTO>> GetUsersInRole(string roleName)
        {
            return _mapper.Map<IList<SRPUserDTO>>(await _userManager.GetUsersInRoleAsync(roleName));
        }

        public async Task<GetUserWithRolesListVm> GetUserWithRolesList(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var currentUser = await _userManager.FindByIdAsync(_currentUserService.UserId.ToString());
            var roles = _roleManager.Roles.ToList();

            var userWithRolesList = new List<AddRoleToUser>();
            foreach (var role in roles)
            {
                if (role.Name == "Unconfirmed" || role.Name == "SuperAdmin")
                    continue;
                if (_currentUserService.Role == "Admin" && role.Name == "Admin" && !await _userManager.IsInRoleAsync(currentUser, "SuperAdmin"))
                    continue;
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userWithRolesList.Add(new AddRoleToUser(role.Name, true));
                }
                else
                {
                    userWithRolesList.Add(new AddRoleToUser(role.Name, false));
                }
            }
            var vm = new GetUserWithRolesListVm()
            {
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                UserWithRoles = userWithRolesList
            };
            return vm;
        }

        public async Task<bool> LockoutUserAsync(Guid id)
        {
            var userToBlock = await _userManager.FindByIdAsync(userId: id.ToString());
            if (userToBlock == null) return false;
            if (userToBlock.Id == _currentUserService.UserId) return false;

            var result = await _userManager.SetLockoutEndDateAsync(userToBlock, DateTimeOffset.MaxValue);

            return result.Succeeded;
        }

        public async Task<bool> ModifyUserRolesAsync(ChangeRoleForUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return false;
            foreach (var userWithRole in request.UserWithRoles)
            {
                if (userWithRole.Add && !await _userManager.IsInRoleAsync(user, userWithRole.Role))
                {
                    var identityResult = await _userManager.AddToRoleAsync(user, userWithRole.Role);
                    if (!identityResult.Succeeded) return false;
                }
                else if (!userWithRole.Add && await _userManager.IsInRoleAsync(user, userWithRole.Role))
                {
                    var response = await _userManager.RemoveFromRoleAsync(user, userWithRole.Role);
                    if (!response.Succeeded) return false;
                }
            }
            return true;
        }

        public async Task SetUserClaim(Guid userId, string claimName, string claimValue)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var claims = await _userManager.GetClaimsAsync(user);
            var claim = claims.FirstOrDefault(x => x.Type.Equals(claimName));
            if (claim == null)
            {
                await _userManager.AddClaimAsync(user, new Claim(claimName, claimValue));
            };
            if (claim != null && claim.Value != claimValue)
            {
                await _userManager.ReplaceClaimAsync(user, claim, new Claim(claimName, claimValue));
            }
        }

        public async Task<bool> UnlockUser(Guid id)
        {
            var userToUnlock = await _userManager.FindByIdAsync(id.ToString());
            if (userToUnlock == null) return false;
            if (userToUnlock.Id == _currentUserService.UserId) return false;

            var result = await _userManager.SetLockoutEndDateAsync(userToUnlock, null);

            return result.Succeeded;
        }
    }
}
