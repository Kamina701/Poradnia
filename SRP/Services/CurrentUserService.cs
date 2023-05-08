using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SRP.interfaces;
using SRP.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SRP.Services
{
    public class CurrentUserService : ICurrentUserService
    {      
        public IHttpContextAccessor Accessor { get; }
        public UserManager<SRPUser> UserManager { get; }
        public SignInManager<SRPUser> SignInManager { get; }
        public CurrentUserService(IHttpContextAccessor accessor, UserManager<SRPUser> userManager, SignInManager<SRPUser> signInManager)
        {
            Accessor = accessor;
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public Guid UserId
        {
            get
            {
                Guid.TryParse(Accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out var res);
                return res;
            }
        }
        public string UserName => Accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);

        public bool IsAdmin => Accessor.HttpContext.User.IsInRole("Admin");
        public string Role => Accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role))?.Value;

        public string mailAddress => Accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value;




        public async Task<string> GetClaim(string name)
        {
            var claim = Accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(name));
            return claim?.Value;
        }

        public async Task SetClaim(string name, string value)
        {
            var claim = Accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(name));
            var user = await UserManager.FindByIdAsync(this.UserId.ToString());

            if (claim == null)
            {
                await UserManager.AddClaimAsync(user, new Claim(name, value));
            };
            if (claim != null && claim.Value != value)
            {
                await UserManager.ReplaceClaimAsync(user, claim, new Claim(name, value));
            }
            await SignInManager.RefreshSignInAsync(user);
        }
    }
}
