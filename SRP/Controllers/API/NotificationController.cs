using Application.CQRS.Notifications.Commands.MarkAsReadCommand;
using Application.CQRS.Notifications.Queries;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace SRP.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationController : BaseController
    {
        [HttpGet]
        public async Task<IList<NotificationDTO>> GetNewNotifications()
        {
            return await Mediator.Send(new GetNewNotificationsQuery());
        }
        [HttpPost]
        public async Task<IActionResult> MarkAsRead()
        {
            await Mediator.Send(new MarkAsReadCommand());
            return NoContent();
        }
    }
}
