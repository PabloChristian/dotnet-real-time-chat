using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.Shared.Kernel.Handler;
using Real.Time.Chat.Shared.Kernel.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Real.Time.Chat.Web.API.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        protected readonly DomainNotificationHandler _notifications;
        protected readonly IMediatorHandler _mediator;

        protected BaseController(INotificationHandler<DomainNotification> notifications,
                                IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();
        protected bool IsValidOperation() => !_notifications.HasNotifications();

        protected new IActionResult Response(object result)
        {
            if (IsValidOperation())
            {
                return Ok(new ApiOkReturn
                {
                    Success = true,
                    Data = result
                });
            }

            return BadRequest(new ApiBadReturn
            {
                Success = false,
                Errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected void NotifyModelStateErrors()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected void NotifyError(string code, string message) => _mediator.RaiseEvent(new DomainNotification(code, message));
    }
}
