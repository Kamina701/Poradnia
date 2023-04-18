using Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SRP.Controllers
{

    public class HomeController : BaseController
    {
        private readonly IChangelogService _changelogService;

        public HomeController(IChangelogService changelogService)
        {
            _changelogService = changelogService;
        }

        public async Task<IActionResult> Index()
        {
           
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }

};