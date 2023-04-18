using Application.Contracts.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddDbContext<SRPDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SRPConnectionString"));
                options.EnableSensitiveDataLogging(true);
            });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            return services;
        }
    }
}
