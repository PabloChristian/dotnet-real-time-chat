using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Real.Time.Chat.Application.Interfaces;
using Real.Time.Chat.Application.SignalR;
using Real.Time.Chat.Domain.Commands;
using Real.Time.Chat.Domain.Commands.Message;
using Real.Time.Chat.Domain.Interfaces.Services;
using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.Shared.Kernel.Handler;
using Real.Time.Chat.Shared.Kernel.Notifications;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Real.Time.Chat.Bot;

namespace Real.Time.Chat.Web.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IHubContext<ChatHub> _chatHub;
        private readonly IQueueMessageService _queueMessageService;
        public UserController(IUserService userService, IHubContext<ChatHub> chatHub, IQueueMessageService queueMessageService, INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator) : base(notifications, mediator)
        {
            _userService = userService;
            _chatHub = chatHub;
            _queueMessageService = queueMessageService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get() => Response(await Task.Run(() => _userService.GetUsers()));

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(Guid id) => Response(await Task.Run(() => _userService.GetUser(id)));

        [HttpGet("messages")]
        public async Task<IActionResult> GetMessages() => Response(await Task.Run(() => _userService.GetMessages()));

        [HttpGet("messages/{email}")]
        public async Task<IActionResult> GetMessages(string email)
        {
            var messages = await Task.Run(() => _userService.GetMessages(email));

            if (messages.Any())
            {
                return Response(messages.Select(x => new MessageDto
                {
                    Consumer = x.Consumer,
                    Message = x.Message,
                    Sender = x.Sender,
                    Date = x.Date
                }));
            }

            return Response(messages);
        }

        [HttpPost("send")]
        [Authorize]
        public async Task<IActionResult> SendMessage([FromBody]MessageAddCommand messageAddCommand)
        {
            if (!string.IsNullOrEmpty(messageAddCommand.Consumer))
            {
                await _queueMessageService.SendMessageAsync(new MessageDto
                {
                    Consumer = messageAddCommand.Consumer,
                    Date = DateTime.Now,
                    Message = messageAddCommand.Message,
                    Sender = messageAddCommand.Sender
                });
            }
            else
                await _chatHub.Clients.Groups(messageAddCommand.Sender).SendAsync("ReceiveMessage", messageAddCommand.Sender, "Was not delivered. please, select an user");

            return Response(true);
        }

        [HttpPost("receive")]
        [Authorize]
        public async Task<IActionResult> ReceiveMessage([FromBody] MessageDto message)
        {
            var bot = new BotCall();
            if (BotCall.IsStockCall(message.Message))
            {
                var msg = bot.CallServiceStock(message.Message[7..]);
                await _chatHub.Clients.Groups(message.Sender).SendAsync("ReceiveMessage", "Bot", msg);
                if (!string.IsNullOrEmpty(message.Consumer) && bot.VerifyResponse())
                    await _chatHub.Clients.Groups(message.Consumer).SendAsync("ReceiveMessage", "Bot", msg);
            }
            else
            {
                await _mediator.SendCommandResult(new MessageAddCommand { Consumer = message.Consumer, Message = message.Message, Sender = message.Sender });

                if (!string.IsNullOrEmpty(message.Consumer))
                    await _chatHub.Clients.Groups(message.Consumer).SendAsync("ReceiveMessage", message.Sender, message.Message);
                else
                    await _chatHub.Clients.Groups(message.Sender).SendAsync("ReceiveMessage", message.Sender, "Was not delivered. please, select an user");
            }
            return Response(true);
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]UserAddCommand command)
        {
            var result = await _mediator.SendCommandResult(command);

            if (result)
                await _chatHub.Clients.All.SendAsync("NewUserRegistered", new UserDto { Email = command.Email, Name = command.Name });

            return Response(result);
        }
    }
}