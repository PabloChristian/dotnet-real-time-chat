
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
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator, ILogger<IdentityController> logger) 
            : base(notifications, mediator) 
        {
            _logger = logger;
        }

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

            if (token != null)
            {
                _logger.LogInformation($"{command.UserName} logged in");
                return Response(token);
            }

            return Unauthorized();
        }

        /// <summary>
        /// Identity control for logout
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiOkReturn))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> LogoutAsync(LogoutUserCommand command)
        {
            var result = await _mediator.SendCommandResult(command);

            if (result)
                _logger.LogInformation($"{command.UserName} logged out");

            return Response(result);
        }
    }
}