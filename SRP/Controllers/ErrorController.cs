using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SRP.Controllers
{
    public class ErrorController : Controller
    {
        //[ApiExplorerSettings(IgnoreApi = true)]
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            //todo logowac skad przyszedl error
            var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            ViewData["ErrorPathBase"] = feature?.OriginalPath;
            ViewData["ErrorQuerystring"] = feature?.OriginalQueryString;


            switch (statusCode)
            {
                case 404: return PartialView("404");
                case 400: return PartialView("400");
                default: return PartialView("404");
            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/Error")]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var path = exceptionDetails.Path;
            var stack = exceptionDetails.Error.StackTrace;
            var message = exceptionDetails.Error.Message;

            return PartialView("404");
        }
    }
}