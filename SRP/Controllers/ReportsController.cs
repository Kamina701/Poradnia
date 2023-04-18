using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using SRP.Controllers;
using Application.CQRS.Reports.Queries;
using SRP.Models.DTOs;
using Application.CQRS.Reports.Commands;
using SRP.Filters;

namespace App.Controllers
{
    [Authorize]
    public class ReportsController : BaseController
    {

        [Route("[controller]")]
        public async Task<IActionResult> List()
        {
            var result = await Mediator.Send(new GetReportsQuery());
            return View(result);
        }

        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateReportDto reportDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await Mediator.Send(new SaveReportCommand(reportDto.Message, reportDto.Type.Value));
            TempData["Message"] = "Wysłano nowe zgłoszenie";
            return RedirectToAction("List");
        }


        [Route("[controller]/[action]/{id:guid}")]
        [TypeFilter(typeof(ReportFilter))]
        public async Task<IActionResult> Details(Guid Id)
        {
            var result = await Mediator.Send(new GetReportDetailsQuery(Id));
            return View("Details", result);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentDto comment)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Uzupełnij treść odpowiedzi.";
                return RedirectToAction("Details", new { id = comment.ReportId });
            }
            await Mediator.Send(new SaveReportAnswerCommand(comment.ReportId, comment.Message));
            TempData["Message"] = "Dodano odpowiedź.";

            return RedirectToAction("Details", new { id = comment.ReportId });

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Close([FromForm] CloseReportDto report)
        {
            await Mediator.Send(new FinishReportCommand(report.Id));
            return RedirectToAction("List");
        }
    }
}
