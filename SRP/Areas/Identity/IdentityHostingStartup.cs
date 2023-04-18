using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Areas.Identity.IdentityHostingStartup))]
namespace Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}