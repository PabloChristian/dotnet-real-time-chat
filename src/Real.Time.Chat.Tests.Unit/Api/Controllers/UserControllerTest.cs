using AutoMapper;
using Real.Time.Chat.Application.AutoMapper;
using Real.Time.Chat.Domain.Interfaces;
using Real.Time.Chat.Application.Services;
using Real.Time.Chat.Application.SignalR;
using Real.Time.Chat.Domain.Entity;
using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.Shared.Kernel.Handler;
using Real.Time.Chat.Shared.Kernel.Notifications;
using Real.Time.Chat.Infrastructure.Data;
using Real.Time.Chat.Infrastructure.Data.Repositories;
using Real.Time.Chat.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;
using Real.Time.Chat.Domain.Interfaces.Messaging;
using Xunit;
using FluentAssertions;
using Real.Time.Chat.Tests.Fixture;
using Microsoft.Extensions.Logging;

namespace Real.Time.Chat.Tests.Api.Controllers
{
    public class UserControllerTest : RealTimeChatDbContextFixure
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mock<IMediatorHandler> _mockMediator;
        private readonly Mock<IHubContext<MessageChatHub>> _mockChat;
        private readonly Mock<IQueueMessageService> _mockQueue;
        private readonly IUserRepository _userRepository;
        private readonly DomainNotificationHandler _domainNotificationHandler;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<UserController>> _mockLogger;
        private UserController _controller;

        public UserControllerTest()
        {
            db = GetDbInstance();
            _unitOfWork = new UnitOfWork(db);
            _mockChat = new Mock<IHubContext<MessageChatHub>>();
            _userRepository = new UserRepository(db);
            _mockMediator = new Mock<IMediatorHandler>();
            _domainNotificationHandler = new DomainNotificationHandler();
            _mockMediator.Setup(x => x.RaiseEvent(It.IsAny<DomainNotification>())).Callback<DomainNotification>((x) =>
            {
                _domainNotificationHandler.Handle(x, CancellationToken.None);
            });
            _mockQueue = new Mock<IQueueMessageService>();
            _mockQueue.Setup(x => x.SendMessageAsync(It.IsAny<MessageDto>())).Returns(Task.CompletedTask);
            _mockLogger = new Mock<ILogger<UserController>>();

            _mapper = AutoMapperConfig.RegisterMappings().CreateMapper();
        }

        [Fact]
        public async Task Should_not_return_list_of_users()
        {
            //Arrange
            _controller = new UserController(
                new UserService(_userRepository, _mapper), _mockChat.Object, _mockQueue.Object, _domainNotificationHandler, _mockMediator.Object, _mockLogger.Object
            ); ;

            //Act
            var result = (await _controller.Get() as OkObjectResult)?.Value as ApiOkReturn;
            var list = result?.Data as List<UserDto>;

            //Assert
            list?.Count.Should().Be(0);
        }

        [Fact]
        public async Task Should_return_list_of_users()
        {
            //Arrange
            var user = new User
            {
                UserName = "test",
                Password = "123456",
                Name = "test"
            };
            var userTwo = new User
            {
                UserName = "test2",
                Password = "123456",
                Name = "test2"
            };

            var cancellationToken = new CancellationToken();
            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.AddAsync(userTwo, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _controller = new UserController(
                new UserService(_userRepository, _mapper), _mockChat.Object, _mockQueue.Object, _domainNotificationHandler, _mockMediator.Object, _mockLogger.Object
            );

            //Act
            var result = (await _controller.Get() as OkObjectResult)?.Value as ApiOkReturn;
            var list = result?.Data as List<UserDto>;

            //Assert
            list?.Count.Should().Be(2);
        }

        [Fact]
        public async Task Should_not_return_a_user()
        {
            //Arrange
            _controller = new UserController(
                new UserService(_userRepository, _mapper), _mockChat.Object, _mockQueue.Object, _domainNotificationHandler, _mockMediator.Object, _mockLogger.Object
            );

            //Act
            var result = (await _controller.Get(Guid.NewGuid()) as OkObjectResult)?.Value as ApiOkReturn;

            //Assert
            result?.Data.Should().BeNull();
        }

        [Fact]
        public async Task Should_return_a_user()
        {
            //Arrange
            var user = new User
            {
                UserName = "test",
                Password = "123456",
                Name = "test one"
            };
            var userTwo = new User
            {
                UserName = "test2",
                Password = "123456",
                Name = "test two"
            };

            var cancellationToken = new CancellationToken();
            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.AddAsync(userTwo, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _controller = new UserController(
                new UserService(_userRepository, _mapper), _mockChat.Object, _mockQueue.Object, _domainNotificationHandler, _mockMediator.Object, _mockLogger.Object
            );

            //Act
            var result = (await _controller.Get(user.Id) as OkObjectResult)?.Value as ApiOkReturn;
            var getUser = result?.Data as UserDto;

            //Assert
            user.Id.Should().Be(getUser.Id);
        }

        [Fact]
        public async Task Should_not_return_list_of_messages()
        {
            _controller = new UserController(
                new UserService(_userRepository, _mapper), _mockChat.Object, _mockQueue.Object, _domainNotificationHandler, _mockMediator.Object, _mockLogger.Object
            );

            //Act
            var result = (await _controller.GetMessages() as OkObjectResult)?.Value as ApiOkReturn;
            var list = result?.Data as List<Messages>;

            //Assert
            list?.Count.Should().Be(0);
        }

        [Fact]
        public async Task Should_return_list_of_messages()
        {
            //Arrange
            var message = new Messages() 
            { 
                Message = "Hello world one", Sender = "test",  Consumer = "test2" ,
                Date = DateTime.Now
            };

            var messageTwo = new Messages()
            {
                Message = "Hello world Two",
                Sender = "test",
                Consumer = "test2",
                Date = DateTime.Now
            };

            var cancellationToken = new CancellationToken();
            await _userRepository.AddAsync(message, cancellationToken);
            await _userRepository.AddAsync(messageTwo, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _controller = new UserController(
                new UserService(_userRepository, _mapper), _mockChat.Object, _mockQueue.Object, _domainNotificationHandler, _mockMediator.Object, _mockLogger.Object
            );

            var result = (await _controller.GetMessages() as OkObjectResult)?.Value as ApiOkReturn;
            var list = result?.Data as List<Messages>;

            list?.Count.Should().Be(2);
        }
    }
}
