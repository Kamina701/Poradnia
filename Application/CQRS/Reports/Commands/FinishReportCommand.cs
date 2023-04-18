using Application.Contracts.Persistance;
using Application.CQRS;
using Domain.Entities.Autitables;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Reports.Commands
{
    public class FinishReportCommand : AuthorizedRequest, IRequest
    {
        public Guid Id { get; set; }
        public FinishReportCommand(Guid id)
        {
            Id = id;
        }
    }
    public class FinishReportCommandHandler : IRequestHandler<FinishReportCommand>
    {
        private readonly IAsyncRepository<Report> _reportRepository;
        private readonly IAsyncRepository<UserNotification> _userNotificationRepository;

        public FinishReportCommandHandler(IAsyncRepository<Report> reportRepository, IAsyncRepository<UserNotification> userNotificationRepository)
        {
            _reportRepository = reportRepository;
            _userNotificationRepository = userNotificationRepository;
        }

        public async Task<Unit> Handle(FinishReportCommand request, CancellationToken cancellationToken)
        {
            var reportToUpdate = await _reportRepository.GetByIdAsync(request.Id);
            if (reportToUpdate != null) { reportToUpdate.Status = Status.Zakończone; }
            await _reportRepository.UpdateAsync(reportToUpdate);
            var notify = UserNotification.ReportHasBeenClosed(reportToUpdate.Id, reportToUpdate.CreatedBy.Value);
            await _userNotificationRepository.AddAsync(notify);
            return Unit.Value;
        }

    }
}
