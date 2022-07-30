
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Real.Time.Chat.Domain.CommandHandlers;
using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.Shared.Kernel.Handler;
using Real.Time.Chat.Shared.Kernel.Notifications;

namespace Real.Time.Chat.API.Controllers
{
    [ApiController]
    [Route("api/logins")]
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        public LoginController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator) 
            : base(notifications, mediator) { }

        /// <summary>
        /// User's Login
        /// </summary>
        /// <remarks>
        /// POST /login
        /// {
        ///     "email": "test@test.com",
        ///     "password": "password"
        /// }
        /// </remarks>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiOkReturn))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(AuthenticateUserCommand command)
        {
            var token = await _mediator.SendCommandResult(command);

            if (token != null)
                return Response(token);

            return Unauthorized();
        }
    }
}