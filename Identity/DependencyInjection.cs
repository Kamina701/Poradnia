using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Identity.Configurations;
using Identity.Models;
using Identity;

namespace Identity
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SRPIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SRPIdentityConnectionString"));
            });

            services.AddIdentity<SRPUser, SRPRole>(opt =>
                 {
                     opt.SignIn.RequireConfirmedAccount = true;
                     opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                     opt.Password.RequireDigit = false;
                     opt.Password.RequireLowercase = false;
                     opt.Password.RequireUppercase = false;
                     opt.Password.RequireNonAlphanumeric = false;
                     opt.Password.RequiredLength = 0;
                 })
                .AddRoles<SRPRole>()
                .AddEntityFrameworkStores<SRPIdentityDbContext>()
                .AddErrorDescriber<CustomIdentityErrorMessages>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.ConfigureApplicationCookie(opt => { opt.LoginPath = "/Identity/Account/Login"; });
            return services;
        }
    }
}