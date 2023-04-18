using Application.Contracts.Persistance;
using Application.Contracts;
using Domain.Entities.Autitables;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace SRP.Filters
{
    public class AccessAttribute : TypeFilterAttribute
    {
        public AccessAttribute(Type type) : base(type)
        {
        }
    }

    public class AccessFilter : IAsyncActionFilter
    {
        private readonly IAsyncRepository<Access> _accessRepository;
        private readonly ICurrentUserService _currentUserService;

        public AccessFilter(IAsyncRepository<Access> accessRepository, ICurrentUserService currentUserService)
        {
            _accessRepository = accessRepository;
            _currentUserService = currentUserService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var itemId = context.ActionArguments.Values.OfType<Guid>().First();

            var access = await _accessRepository.FindByIncludeAsync(a =>
                a.UserId == _currentUserService.UserId);
            if (access != null && access.Validate())
            {
                await next();
            }
            else
            {
                context.Result = new NotFoundResult();
            }
        }
    }
    public class ReportFilter : IAsyncActionFilter
    {
        private readonly IAsyncRepository<Report> _reportRepository;
        private readonly ICurrentUserService _currentUserService;

        public ReportFilter(IAsyncRepository<Report> reportRepository, ICurrentUserService currentUserService)
        {
            _reportRepository = reportRepository;
            _currentUserService = currentUserService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_currentUserService.IsAdmin)
                await next();
            var reportId = context.ActionArguments.Values.OfType<Guid>().First();

            var reportExist = await _reportRepository.AnyAsync(r =>
                r.CreatedBy == _currentUserService.UserId && r.Id == reportId);
            if (reportExist)
            {
                await next();
            }
            else
            {
                context.Result = new NotFoundResult();
            }
        }
    }
}
