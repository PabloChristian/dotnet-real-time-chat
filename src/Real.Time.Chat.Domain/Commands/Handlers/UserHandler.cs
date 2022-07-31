using AutoMapper;
using Real.Time.Chat.Domain.Commands;
using Real.Time.Chat.Domain.Commands.Message;
using Real.Time.Chat.Domain.Entity;
using Real.Time.Chat.Domain.Interfaces;
using Real.Time.Chat.Domain.Interfaces.Services;
using Real.Time.Chat.Shared.Kernel.Handler;
using Real.Time.Chat.Shared.Kernel.Helper;
using Real.Time.Chat.Shared.Kernel.Notifications;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Real.Time.Chat.Domain.Exceptions;

namespace Real.Time.Chat.Domain.CommandHandlers
{
    public class UserHandler : IRequestHandler<UserAddCommand, bool>, IRequestHandler<MessageAddCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public UserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMediatorHandler mediatorHandler, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public Task<bool> Handle(UserAddCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                try
                {
                    var user = _mapper.Map<User>(request);
                    var userExisted = _userRepository.GetByExpression(x => x.UserName == request.UserName).FirstOrDefault();
                    if (userExisted != null)
                        throw new BusinessException(Properties.Resources.User_AlreadyExists);

                    user.Password = Cryptography.PasswordEncrypt(user.Password);

                    _userRepository.Add(user);
                    var success = _unitOfWork.Commit();

                    return Task.FromResult(success);
                }
                catch(Exception e)
                {
                    _mediatorHandler.RaiseEvent(new DomainNotification("exception", e.Message));
                }
            }
            else
            {
                foreach (var error in request.GetErrors())
                    _mediatorHandler.RaiseEvent(new DomainNotification(error.ErrorCode, error.ErrorMessage));
            }

            return Task.FromResult(false);
        }

        public Task<bool> Handle(MessageAddCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                try
                {
                    var message = _mapper.Map<Messages>(request);
                    _userRepository.Add(message);
                    var success = _unitOfWork.Commit();

                    return Task.FromResult(success);
                }
                catch (Exception e)
                {
                    _mediatorHandler.RaiseEvent(new DomainNotification("exception", e.Message));
                }
            }
            else
            {
                foreach (var error in request.GetErrors())
                    _mediatorHandler.RaiseEvent(new DomainNotification(error.ErrorCode, error.ErrorMessage));
            }

            return Task.FromResult(false);
        }
    }
}
