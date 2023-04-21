using Application.Contracts;
using Application.Contracts.Identity;
using Application.Contracts.Persistance;
using Application.CQRS;
using Domain.Entities.Autitables;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace Application.CQRS.Reports.Commands
{
    public class SaveReportCommand : AuthorizedRequest, IRequest
    {
        public SaveReportCommand(string message, ReportType type)
        {
            Message = message;
            Type = type;
        }

        public string Message { get; }
        public ReportType Type { get; }
    }
    public class SaveReportHandler : IRequestHandler<SaveReportCommand>
    {
        private readonly IAsyncRepository<Report> _reportRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAsyncRepository<UserNotification> _userNotyficationRepository;
        private readonly IUsersService _usersService;

        public SaveReportHandler(ICurrentUserService currentUserService, IAsyncRepository<Report> reportRepository,
            IAsyncRepository<UserNotification> userNotyficationRepository, IUsersService usersService)
        {
            _currentUserService = currentUserService;
            _reportRepository = reportRepository;
            _userNotyficationRepository = userNotyficationRepository;
            _usersService = usersService;
        }

        public async Task<Unit> Handle(SaveReportCommand request, CancellationToken cancellationToken)
        {
            var report = new Report
            {
                CreatedBy = request.UserId,
                Message = request.Message,
                Type = request.Type,
                Created = DateTime.Now
            };
            await _reportRepository.AddAsync(report);

            var admins = await _usersService.GetUsersInRole("Admin, SuperAdmin, Doctor");
            Notification notification = Notification.NewReport(_currentUserService.UserName, _currentUserService.UserId);
            foreach (var admin in admins)
            {
                var newUserReport = UserNotification.NewReport(notification, admin.Id);
                await _userNotyficationRepository.AddAsync(newUserReport);
            }
            return Unit.Value;
        }


    }
}
