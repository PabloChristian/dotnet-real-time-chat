using AutoMapper;
using Real.Time.Chat.Domain.Interfaces;
using Real.Time.Chat.Domain.Interfaces.Services;
using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.Shared.Kernel.Handler;
using Real.Time.Chat.Shared.Kernel.Notifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Real.Time.Chat.Domain.CommandHandlers
{
    public class LoginHandler : IRequestHandler<AuthenticateUserCommand, TokenJWT>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ILoginService _loginService;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public LoginHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMediatorHandler mediatorHandler, IMapper mapper, ILoginService loginService)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
            _loginService = loginService;
        }

        public Task<TokenJWT> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            TokenJWT? token = null;

            if (request.IsValid())
            {
                try
                {
                    var user = _loginService.Authenticate(request.Email, request.Password);

                    if (user != null)
                    {
                        token = _loginService.GetToken(user.Id, user.Email);

                        _unitOfWork.Commit();
                    }
                    else
                        _mediatorHandler.RaiseEvent(new DomainNotification("Error", "User not founded"));
                }
                catch (Exception e)
                {
                    _mediatorHandler.RaiseEvent(new DomainNotification("Exception", e.Message));
                }
            }
            else
                foreach (var error in request.GetErrors())
                    _mediatorHandler.RaiseEvent(new DomainNotification(error.ErrorCode, error.ErrorMessage));

            return Task.FromResult(token);
        }
    }
}
