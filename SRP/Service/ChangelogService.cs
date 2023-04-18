using Application.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System;

namespace SRP.Service
{
    public class ChangelogService : IChangelogService
    {
        private readonly IWebHostEnvironment _environment;

        public ChangelogService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<Changelog> GetChangesAsync()
        {
            try
            {

                var content = await File.ReadAllTextAsync(Path.Combine(_environment.WebRootPath, "changelog.json"),
                    Encoding.UTF8);
                var changes = JsonConvert.DeserializeObject<Changelog>(content);
                return changes;
            }
            catch (Exception e)
            {
            }

            return null;
        }
    }
}
