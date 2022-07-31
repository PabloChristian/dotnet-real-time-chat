using Real.Time.Chat.Domain.Interfaces;
using Real.Time.Chat.Domain.Interfaces.Services;
using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.Shared.Kernel.Handler;
using Real.Time.Chat.Shared.Kernel.Notifications;
using MediatR;

namespace Real.Time.Chat.Domain.CommandHandlers
{
    public class IdentityHandler : IRequestHandler<AuthenticateUserCommand, TokenJWT>, IRequestHandler<LogoutUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdentityService _identityService;
        private readonly IMediatorHandler _mediatorHandler;

        public IdentityHandler(IUnitOfWork unitOfWork, IMediatorHandler mediatorHandler, IIdentityService loginService)
        {
            _unitOfWork = unitOfWork;
            _mediatorHandler = mediatorHandler;
            _identityService = loginService;
        }

        public async Task<TokenJWT> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            TokenJWT token = new(true,string.Empty);

            if (request.IsValid())
            {
                try
                {
                    var user = _identityService.Authenticate(request.UserName, request.Password);

                    if (user != null)
                    {
                        token = _identityService.GetToken(user.Id, user.UserName);
                        await _unitOfWork.CommitAsync(cancellationToken);
                    }
                    else
                        await _mediatorHandler.RaiseEvent(new DomainNotification("Error", Properties.Resources.User_NotFound));
                }
                catch (Exception e)
                {
                    await _mediatorHandler.RaiseEvent(new DomainNotification("Exception", e.Message));
                }
            }
            else
                foreach (var error in request.GetErrors())
                    await _mediatorHandler.RaiseEvent(new DomainNotification(error.ErrorCode, error.ErrorMessage));

            return await Task.FromResult(token);
        }

        public async Task<bool> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            var result = false;

            if (request.IsValid())
            {
                try
                {
                    var userLoggedout = _identityService.Logout(request.UserName);

                    if (userLoggedout != null)
                    {
                        result = await _unitOfWork.CommitAsync(cancellationToken);
                    }
                    else
                        await _mediatorHandler.RaiseEvent(new DomainNotification("Error", Properties.Resources.User_NotFound));
                }
                catch (Exception e)
                {
                    await _mediatorHandler.RaiseEvent(new DomainNotification("Exception", e.Message));
                }
            }
            else
                foreach (var error in request.GetErrors())
                    await _mediatorHandler.RaiseEvent(new DomainNotification(error.ErrorCode, error.ErrorMessage));

            return await Task.FromResult(result);
        }
    }
}
