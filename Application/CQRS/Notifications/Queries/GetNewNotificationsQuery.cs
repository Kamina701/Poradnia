using Application.Contracts;
using Application.Contracts.Persistance;
using Application.DTOs;
using AutoMapper;
using Domain.Entities.Autitables;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Application.CQRS.Notifications.Queries
{
    public class GetNewNotificationsQuery : IRequest<IList<NotificationDTO>>
    {
    }
    public class GetNewNotificationsHandler : IRequestHandler<GetNewNotificationsQuery, IList<NotificationDTO>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Notification> _notificationRepository;

        public GetNewNotificationsHandler(ICurrentUserService currentUserService, IMapper mapper, IAsyncRepository<Notification> notificationRepository)
        {
            _currentUserService = currentUserService;
            _mapper = mapper;
            _notificationRepository = notificationRepository;
        }

        public async Task<IList<NotificationDTO>> Handle(GetNewNotificationsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var notifications = await _notificationRepository.FindManyByIncludeAsync(
                n => n.UserNotifications.Any(un => !un.IsRead && un.UserId == userId), u => u.UserNotifications);
            var result = _mapper.Map<IList<Notification>, IList<NotificationDTO>>(notifications);
            return result;
        }
    }
}