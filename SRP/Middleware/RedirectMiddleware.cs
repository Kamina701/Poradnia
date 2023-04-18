using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SRP.Middleware
{
    public class RedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/Identity/Account/Login" || context.Request.Path == "/Identity/Account/Register")
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    context.Response.Redirect("/Home/Index");
                }
            }
            await _next(context);
        }
    }
}
