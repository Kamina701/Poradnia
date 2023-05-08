using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SRP.Configurations;
using SRP.interfaces;
using SRP.Interfaces;
using SRP.Models;
using SRP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;

namespace SRP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddDbContext<SrpDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<SRPUser, SRPRole>(opt =>
            {
                opt.SignIn.RequireConfirmedAccount = true;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 0;
            })
                .AddRoles<SRPRole>()
                .AddEntityFrameworkStores<SrpDbContext>()
                .AddErrorDescriber<CustomIdentityErrorMessages>();

            services.ConfigureApplicationCookie(opt => { opt.LoginPath = "/Identity/Account/Login"; });
            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddControllersWithViews();
            services.AddAuthentication("cookie")
               .AddCookie("cookie", opt => { opt.Cookie.SameSite = SameSiteMode.Strict; });
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("ManageUsers", policy =>
                {
                    policy.RequireRole("Admin", "SuperAdmin");
                });
                opt.AddPolicy("ManageAccess", policy =>
                {
                    policy.RequireRole("Admin", "SuperAdmin");
                });
                opt.AddPolicy("SpecialPrivilegesOnly", policy =>
                {
                    policy.RequireClaim("SpecialPrivileges");
                });
            });
            services.Configure<IISServerOptions>(opt => { opt.MaxRequestBodySize = int.MaxValue; });
            services.AddSignalR();
            services.Configure<FormOptions>(opt =>
            {
                opt.ValueCountLimit = Int32.MaxValue;
            });
            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            using (var scope =
                        app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetService<SrpDbContext>())
                context.Database.Migrate();

            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
