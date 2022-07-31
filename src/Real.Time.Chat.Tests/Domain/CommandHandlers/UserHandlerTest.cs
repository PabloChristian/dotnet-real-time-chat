using AutoMapper;
using Real.Time.Chat.Application.AutoMapper;
using Real.Time.Chat.Domain.CommandHandlers;
using Real.Time.Chat.Domain.Commands;
using Real.Time.Chat.Domain.Commands.Message;
using Real.Time.Chat.Domain.Entity;
using Real.Time.Chat.Domain.Interfaces;
using Real.Time.Chat.Shared.Kernel.Handler;
using Real.Time.Chat.Shared.Kernel.Notifications;
using Real.Time.Chat.Infrastructure.Data;
using Real.Time.Chat.Infrastructure.Data.Repositories;
using Moq;
using Xunit;
using FluentAssertions;
using Real.Time.Chat.Tests.Fixture;

namespace Real.Time.Chat.Tests.Domain.CommandHandlers
{
    public class UserHandlerTest : RealTimeChatDbContextFixure
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mock<IMediatorHandler> _mockMediator;
        private readonly IUserRepository _userRepository;
        private readonly DomainNotificationHandler _domainNotificationHandler;
        private readonly IMapper _mapper;
        private readonly UserHandler handler;

        public UserHandlerTest()
        {
            db = GetDbInstance();
            _unitOfWork = new UnitOfWork(db);
            _userRepository = new UserRepository(db);
            _mockMediator = new Mock<IMediatorHandler>();
            _domainNotificationHandler = new DomainNotificationHandler();
            _mockMediator.Setup(x => x.RaiseEvent(It.IsAny<DomainNotification>())).Callback<DomainNotification>((x) =>
            {
                _domainNotificationHandler.Handle(x, CancellationToken.None);
            });
            _mapper = AutoMapperConfig.RegisterMappings().CreateMapper();
            handler = new UserHandler(_unitOfWork, _userRepository, _mockMediator.Object, _mapper);
        }

        [Fact]
        public async Task Should_not_register_user_name_is_required()
        {
            //Arrange
            var user = new UserAddCommand
            {
                UserName = "guest",
                Password = "123456",
                SecondPassword = "123456"
            };

            //Act
            var result = await handler.Handle(user, CancellationToken.None);

            //Arrange
            result.Should().BeFalse();
            _domainNotificationHandler.HasNotifications().Should().BeTrue();
        }

        [Fact]
        public async Task Should_not_register_username_is_invalid()
        {
            //Arrange
            var user = new UserAddCommand
            {
                UserName = "",
                Password = "123456",
                SecondPassword = "123456",
                Name =  "Pablo"
            };

            //Act
            var result = await handler.Handle(user, CancellationToken.None);

            //Arrange
            result.Should().BeFalse();
            _domainNotificationHandler.GetNotifications().Any().Should().BeTrue();
        }

        [Fact]
        public async Task Should_not_register_password_are_not_equal()
        {
            //Arrange
            string expectedMessageError = "The passwords are not equal";
            var user = new UserAddCommand
            {
                UserName = "guest",
                Password = "123456",
                SecondPassword = "123465",
                Name = "Pablo"
            };

            //Act
            var result = await handler.Handle(user, CancellationToken.None);

            //Arrange
            result.Should().BeFalse();
            _domainNotificationHandler.GetNotifications().Any(x => x.Value == expectedMessageError).Should().BeTrue();
        }

        [Fact]
        public async Task Should_not_register_password_have_less_than_six()
        {
            //Arrange
            var user = new UserAddCommand
            {
                UserName = "guest",
                Password = "12345",
                SecondPassword = "12345",
                Name = "Pablo"
            };

            //Act
            var result = await handler.Handle(user, CancellationToken.None);

            //Arrange
            result.Should().BeFalse();
            _domainNotificationHandler.GetNotifications().Any().Should().BeTrue();
        }

        [Fact]
        public async Task Should_register_user_valid()
        {
            //Arrange
            var user = new UserAddCommand
            {
                UserName = "guest",
                Password = "123456",
                SecondPassword = "123456",
                Name = "Pablo"
            };

            //Act
            var result = await handler.Handle(user, CancellationToken.None);

            //Arrange
            result.Should().BeTrue();
            _domainNotificationHandler.HasNotifications().Should().BeFalse();
        }

        [Fact]
        public async Task Should_not_register_message_field_message_is_required()
        {
            //Arrange
            var message = new MessageAddCommand()
            {
                Sender = "snakepablo@hotmail.com"
            };

            //Act
            var result = await handler.Handle(message, CancellationToken.None);

            //Arrange
            result.Should().BeFalse();
            _domainNotificationHandler.GetNotifications().Any().Should().BeTrue();
        }

        [Fact]
        public async Task Should_not_register_message_field_sender_is_required()
        {
            //Arrange
            var message = new MessageAddCommand()
            {
                Message = "Hello world"
            };

            //Act
            var result = await handler.Handle(message, CancellationToken.None);

            //Arrange
            result.Should().BeFalse();
            _domainNotificationHandler.GetNotifications().Any().Should().BeTrue();
        }

        [Fact]
        public async Task Should_register_messsage()
        {
            //Arrange
            var message = new MessageAddCommand()
            {
                Message = "Hello world",
                Sender = "snakepablo@hotmail.com"
            };

            //Act
            var result = await handler.Handle(message, CancellationToken.None);

            //Arrange
            result.Should().BeTrue();
            _domainNotificationHandler.HasNotifications().Should().BeFalse();
        }
    }
}
