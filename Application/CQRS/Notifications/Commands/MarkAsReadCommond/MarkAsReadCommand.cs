using MediatR;
using System.Threading;
using System.Threading.Tasks;

using Application.Contracts;
using Domain.Entities.Autitables;
using Application.Contracts.Persistance;

namespace Application.CQRS.Notifications.Commands.MarkAsReadCommand
{
    public class MarkAsReadCommand : IRequest
    {
    }

    public class MarkAsReadCommandHandler : IRequestHandler<MarkAsReadCommand>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IAsyncRepository<UserNotification> _userNotificationRepository;


        public MarkAsReadCommandHandler(
             ICurrentUserService currentUserService, IAsyncRepository<UserNotification> userNotificationRepository)
        {
            _currentUserService = currentUserService;
            _userNotificationRepository = userNotificationRepository;
        }
        public async Task<Unit> Handle(MarkAsReadCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var unreadNotifications = await _userNotificationRepository.FindByAsync(un => !un.IsRead && un.UserId == userId);
            foreach (var unreadNotification in unreadNotifications)
            {
                unreadNotification.Read();
            }
            await _userNotificationRepository.Save();
            return Unit.Value;
        }
    }
}
