using Application.Contracts.Identity;
using Application.Contracts;
using Application;
using Identity.Service;
using Identity;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistance;
using SRP.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.FeatureManagement;
using Microsoft.AspNetCore.Http;
using Application.Contracts.Infrastructure;
using Infrastructure.Service;
using SRP.Middleware;

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
            services.RegisterApplicationServices();
            services.RegisterIdentityServices(Configuration);
            services.RegisterInfrastructureServices(Configuration);
            services.RegisterPersistenceServices(Configuration);
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IChangelogService, ChangelogService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddMemoryCache();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => { builder.AllowAnyOrigin(); });
            });

            services.AddAuthentication("cookie")
                .AddCookie("cookie", opt => { opt.Cookie.SameSite = SameSiteMode.Strict; });
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("ManageUsers", policy =>
                {
                    policy.RequireRole( "Admin", "SuperAdmin");
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


            services.AddRazorPages();
            services.Configure<IISServerOptions>(opt => { opt.MaxRequestBodySize = int.MaxValue; });
            services.AddSignalR();
            services.AddFeatureManagement();
            services.AddMvc(opt =>
            {
                opt.MaxModelBindingCollectionSize = Int32.MaxValue;

            });
            services.Configure<FormOptions>(opt =>
            {
                opt.ValueCountLimit = Int32.MaxValue;
            });


            services.AddResponseCaching();
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
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseMiddleware<RedirectMiddleware>();
            app.UseCors("AllowAnyOrigin");
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
