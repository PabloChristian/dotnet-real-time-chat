
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Real.Time.Chat.Application.SignalR;
using Real.Time.Chat.Domain.CommandHandlers;
using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.Shared.Kernel.Handler;
using Real.Time.Chat.Shared.Kernel.Notifications;

namespace Real.Time.Chat.Api.Controllers
{
    [ApiController]
    [Route("api/identity")]
    [AllowAnonymous]
    public class IdentityController : BaseController
    {

        public IdentityController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator) 
            : base(notifications, mediator) { }

        /// <summary>
        /// Identity control for login
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiOkReturn))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(AuthenticateUserCommand command)
        {
            var token = await _mediator.SendCommandResult(command);

            if (token != null) return Response(token);

            return Unauthorized();
        }
    }
}