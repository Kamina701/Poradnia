using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using Identity.Models;
using Identity;
using System.Linq;
using Application.Contracts;

namespace SRP.Service
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<SRPUser> _userManager;
        private readonly SignInManager<SRPUser> _signInManager;

        public CurrentUserService(IHttpContextAccessor accessor, SRPIdentityDbContext SRPIdentityDbContext, UserManager<SRPUser> userManager, SignInManager<SRPUser> signInManager)
        {
            _accessor = accessor;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public Guid UserId
        {
            get
            {
                Guid.TryParse(_accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out var res);
                return res;
            }
        }

        public string UserName => _accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        public bool IsAdmin => _accessor.HttpContext.User.IsInRole("Admin");
        public string Role => _accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role))?.Value;


        public async Task SetClaim(string name, string value)
        {
            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(name));
            var user = await _userManager.FindByIdAsync(this.UserId.ToString());

            if (claim == null)
            {
                await _userManager.AddClaimAsync(user, new Claim(name, value));
            };
            if (claim != null && claim.Value != value)
            {
                await _userManager.ReplaceClaimAsync(user, claim, new Claim(name, value));
            }
            await _signInManager.RefreshSignInAsync(user);
        }
        public async Task<string> GetClaim(string name)
        {
            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(name));
            return claim?.Value;
        }
    }
}
