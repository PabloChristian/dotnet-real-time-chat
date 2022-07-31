using Real.Time.Chat.Domain.CommandHandlers;
using Real.Time.Chat.Domain.Commands;
using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.Shared.Kernel.Handler;
using Real.Time.Chat.Shared.Kernel.Notifications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Xunit;
using FluentAssertions;
using Real.Time.Chat.Api.Controllers;
using Real.Time.Chat.Tests.Fixture;

namespace Real.Time.Chat.Tests.Api.Controllers
{
    public class LoginControllerTest : RealTimeChatDbContextFixure
    {
        private readonly Mock<IMediatorHandler> _mockMediator;
        private readonly DomainNotificationHandler _domainNotificationHandler;

        public LoginControllerTest()
        {
            _mockMediator = new Mock<IMediatorHandler>();
            _domainNotificationHandler = new DomainNotificationHandler();
            _mockMediator.Setup(x => x.RaiseEvent(It.IsAny<DomainNotification>())).Callback<DomainNotification>((x) =>
            {
                _domainNotificationHandler.Handle(x, CancellationToken.None);
            });
        }

        [Fact]
        public async Task Should_not_get_authenticated_return_unathourized()
        {
            //Arrange
            var obj = new AuthenticateUserCommand { UserName = "test", Password = "123" };
            _mockMediator.Setup(x => x.SendCommandResult(It.IsAny<GenericCommandResult<bool>>())).Returns(Task.FromResult(false));
            
            //Act
            var result = await new LoginController(_domainNotificationHandler, _mockMediator.Object).LoginAsync(obj) as UnauthorizedResult;

            //Assert
            result?.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Should_get_authenticated_token()
        {
            //Arrange
            string tokenExpected = "asASDNdBNASbdaskjdbabksdavbsklDAPsdh";
            var obj = new AuthenticateUserCommand { UserName = "pablo", Password = "123456" };
            _mockMediator.Setup(x => x.SendCommandResult(It.IsAny<GenericCommandResult<TokenJWT>>())).Returns(Task.FromResult(new TokenJWT
            (
                true,
                "asASDNdBNASbdaskjdbabksdavbsklDAPsdh"
            )));

            //Act
            var result = (await new LoginController(_domainNotificationHandler, _mockMediator.Object).LoginAsync(obj) as OkObjectResult)?.Value as ApiOkReturn;
            var token = result?.Data as TokenJWT;

            //Assert
            result?.Success.Should().BeTrue();
            tokenExpected.Should().Be(token?.Token);
        }
    }
}
