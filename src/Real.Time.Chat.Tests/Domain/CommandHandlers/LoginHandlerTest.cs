using AutoMapper;
using Real.Time.Chat.Application.AutoMapper;
using Real.Time.Chat.Application.Services;
using Real.Time.Chat.Domain.CommandHandlers;
using Real.Time.Chat.Domain.Entity;
using Real.Time.Chat.Domain.Interfaces;
using Real.Time.Chat.Domain.Interfaces.Services;
using Real.Time.Chat.Shared.Kernel.Handler;
using Real.Time.Chat.Shared.Kernel.Helper;
using Real.Time.Chat.Shared.Kernel.Notifications;
using Real.Time.Chat.Infrastructure.Data;
using Real.Time.Chat.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using FluentAssertions;
using Real.Time.Chat.Tests.Fixture;

namespace Real.Time.Chat.Tests.Domain.CommandHandlers
{
    public class LoginHandlerTest : RealTimeChatDbContextFixure
    {
        private IUnitOfWork _unitOfWork;
        private Mock<IMediatorHandler> _mockMediator;
        private IUserRepository _userRepository;
        private ILoginService _loginService;
        private DomainNotificationHandler _domainNotificationHandler;
        private IMapper _mapper;
        private LoginHandler handler;

        public LoginHandlerTest()
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

            _userRepository.Add(new User
            {
                UserName = "test",
                Name = "Test",
                Password = Cryptography.PasswordEncrypt("123456")
            });
            _unitOfWork.Commit();

            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x[It.Is<string>(s => s.Equals("Jwt:Issuer"))]).Returns("Test");
            mockConfig.Setup(x => x[It.Is<string>(s => s.Equals("Jwt:Duration"))]).Returns("120");
            mockConfig.Setup(x => x[It.Is<string>(s => s.Equals("Jwt:Key"))]).Returns("IZpipYfLNJro403p");

            _loginService = new LoginService(_userRepository, mockConfig.Object);
            handler = new LoginHandler(_unitOfWork, _userRepository, _mockMediator.Object, _mapper, _loginService);
        }

        [Fact]
        public async Task Should_not_get_authenticated_invalid_username()
        {
            //Arrange
            var userAuth = new AuthenticateUserCommand { UserName = "", Password = "123356" };

            //Act
            var result = await handler.Handle(userAuth, CancellationToken.None);

            //Assert
            result.Token.Should().BeNullOrEmpty();
            _domainNotificationHandler.HasNotifications().Should().BeTrue();
        }

        [Fact]
        public async Task Should_not_get_authenticated_invalid_password()
        {
            //Arrange
            var userAuth = new AuthenticateUserCommand { UserName = "test", Password = "" };

            //Act
            var result = await handler.Handle(userAuth, CancellationToken.None);

            //Assert
            result.Token.Should().BeNullOrEmpty();
            _domainNotificationHandler.HasNotifications().Should().BeTrue();
        }

        [Fact]
        public async Task Should_get_authenticated()
        {
            //Arrange
            var userAuth = new AuthenticateUserCommand { UserName = "test", Password = "123456" };

            //Act
            var result = await handler.Handle(userAuth, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            _domainNotificationHandler.HasNotifications().Should().BeFalse();
        }
    }
}
