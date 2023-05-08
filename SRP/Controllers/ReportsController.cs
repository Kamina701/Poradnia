using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;
using SRP.Models.DTOs;
using SRP.Filters;
using SRP.Interfaces;
using SRP.Models.Enties;
using SRP.interfaces;
using AutoMapper;
using ReportDetails = SRP.Models.ReportDetails;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using SRP.Models;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SRP.Services;
using System.Xml.Linq;

namespace SRP.Controllers
{
    [Authorize]
    public class ReportsController : BaseController
    {
        private readonly IMapper _mapper;
        public IServiceScope Scope { get; }
        public ReportsController(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            _mapper = mapper;
            Scope = serviceScopeFactory.CreateScope();
        }

        [Route("[controller]")]
        public async Task<IActionResult> List()
        {
            var scope = Scope.ServiceProvider.CreateScope();
            using (scope)
            {
                var _reportRepository = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Report>>();
                var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<SRPUser>>();
                var user = _userManager.Users.FirstOrDefault(x => x.UserName == HttpContext.User.Identity.Name);
                if (HttpContext.User.IsInRole("Admin")|| HttpContext.User.IsInRole("SuperAdmin")|| HttpContext.User.IsInRole("Doctor"))
                {
                    var allReports = await _reportRepository.GetAllAsync();
                    return View(_mapper.Map<IList<ReportDto>>(allReports));

                }
                else
                {
                    var userReports = await _reportRepository.FindManyByIncludeAsync(x => x.CreatedBy == user.Id);
                    return View(_mapper.Map<IList<ReportDto>>(userReports));
                }
            }
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReportDto reportDto)
        {
            var scope = Scope.ServiceProvider.CreateScope();
            using (scope)
            {

                var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<SRPUser>>();
                var user = _userManager.Users.FirstOrDefault(x => x.UserName == HttpContext.User.Identity.Name);
                var _reportRepository = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Report>>();
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var report = new Report
                {
                    CreatedBy = user.Id,
                    Message = reportDto.Message,
                    Type = reportDto.Type.Value,
                    Created = DateTime.Now
                };

                await _reportRepository.AddAsync(report);
                TempData["Message"] = "Wysłano nowe zgłoszenie";
                return RedirectToAction("List");
            }
        }


        [Route("[controller]/[action]/{id:guid}")]
        [TypeFilter(typeof(ReportFilter))]
        public async Task<IActionResult> Details(Guid Id)
        {
            var scope = Scope.ServiceProvider.CreateScope();
            using (scope)
            {
                var _reportRepository = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Report>>();
                var reportWithDetails = _reportRepository.GetAll(inc=>inc.Comments).Where(r => r.Id == Id).ToList();
                Report report=null;
                foreach (var reportWithDetail in reportWithDetails)
                {
                    report = reportWithDetail;

                }
                return View("Details", _mapper.Map<ReportDetails>(report));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentDto comment)
        {
            var scope = Scope.ServiceProvider.CreateScope();
            using (scope)
            {
                var _reportRepository = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Report>>();
                var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<SRPUser>>();
                var user = _userManager.Users.FirstOrDefault(x => x.UserName == HttpContext.User.Identity.Name);

                if (!ModelState.IsValid)
                {
                    TempData["Message"] = "Uzupełnij treść odpowiedzi.";
                    return RedirectToAction("Details", new { id = comment.ReportId });
                }

                var report = _reportRepository.GetAll(inc=>inc.Comments).FirstOrDefault(r => r.Id == comment.ReportId);
                
                var c =( new Comment
                {
                    Report = report,
                    ReportId = report.Id,
                    Response = comment.Message,
                    Created = DateTime.Now,
                    CreatedBy = user.Id,
                });
                report.Comments.Add(c);
                await _reportRepository.UpdateAsync(report);
                TempData["Message"] = "Dodano odpowiedź.";

                return RedirectToAction("Details", new { id = comment.ReportId });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin, Doctor")]
        public async Task<IActionResult> Close([FromForm] CloseReportDto report)
        {
            var scope = Scope.ServiceProvider.CreateScope();
            using (scope)
            {
                var _reportRepository = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Report>>();
                var reportToUpdate = await _reportRepository.GetByIdAsync(report.Id);
                if (reportToUpdate != null)
                {
                    reportToUpdate.Status = Status.Zakończone;
                }

                await _reportRepository.UpdateAsync(reportToUpdate);
                return RedirectToAction("List");
            }
        }
    }
}
