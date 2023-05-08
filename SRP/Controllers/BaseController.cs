using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace SRP.Controllers
{
        public class BaseController : Controller
        {
            private ISender _mediator;
            protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
        }
    
}
