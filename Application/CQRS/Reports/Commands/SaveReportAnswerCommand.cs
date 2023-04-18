using Application.Contracts;
using Application.Contracts.Identity;
using Application.Contracts.Persistance;
using Application.CQRS;
using Domain.Entities.Autitables;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Reports.Commands
{
    public class SaveReportAnswerCommand : AuthorizedRequest, IRequest
    {
        public Guid ReportId { get; }
        public string Comment { get; }


        public SaveReportAnswerCommand(Guid reportId, string comment)
        {
            ReportId = reportId;
            this.Comment = comment;
        }
    }
    public class SaveReportAnswerHandler : IRequestHandler<SaveReportAnswerCommand>
    {
        private readonly IAsyncRepository<Comment> _commentRepository;
        private readonly IAsyncRepository<Report> _reportRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAsyncRepository<UserNotification> _userNotificationRepository;
        private readonly IUsersService _usersService;

        public SaveReportAnswerHandler(ICurrentUserService currentUserService, IAsyncRepository<Comment> commentRepository,
            IAsyncRepository<Report> reportRepository, IAsyncRepository<UserNotification> userNotyficationRepository, IUsersService usersService)
        {
            _currentUserService = currentUserService;
            _commentRepository = commentRepository;
            _reportRepository = reportRepository;
            _userNotificationRepository = userNotyficationRepository;
            _usersService = usersService;
        }

        public async Task<Unit> Handle(SaveReportAnswerCommand request, CancellationToken cancellationToken)
        {
            var report = await _reportRepository.FindByIncludeAsync(r => r.Id == request.ReportId, inc => inc.Comments);
            
            report.AddComment(request.Comment, request.UserId);

            await _reportRepository.UpdateAsync(report);

            if (_currentUserService.IsAdmin)
            {
                var notifyUser = UserNotification.AdminRepliedToReport(report.Id, report.CreatedBy.Value);
                await _userNotificationRepository.AddAsync(notifyUser);
            }
            else
            {
                var admins = await _usersService.GetUsersInRole("Admin");
                Notification notification = Notification.UserRepliedToOpenedReport(report.Id);
                foreach (var admin in admins)
                {
                    var newUserReport = UserNotification.UserRepliedToOpenedReport(notification, admin.Id);
                    await _userNotificationRepository.AddAsync(newUserReport);
                }
            }

            return Unit.Value;

        }

    }
}
