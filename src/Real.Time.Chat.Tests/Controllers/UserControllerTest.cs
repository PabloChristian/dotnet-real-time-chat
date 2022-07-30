using AutoMapper;
using Real.Time.Chat.Application.AutoMapper;
using Real.Time.Chat.Application.Interfaces;
using Real.Time.Chat.Application.Services;
using Real.Time.Chat.Application.SignalR;
using Real.Time.Chat.Domain.Entity;
using Real.Time.Chat.Domain.Interfaces;
using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.Shared.Kernel.Handler;
using Real.Time.Chat.Shared.Kernel.Notifications;
using Real.Time.Chat.Infrastructure.Data;
using Real.Time.Chat.Infrastructure.Data.Repositories;
using Real.Time.Chat.Tests.ContextDb;
using Real.Time.Chat.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Real.Time.Chat.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest : RealTimeChatDbContextFixure
    {
        private IUnitOfWork _unitOfWork;
        private Mock<IMediatorHandler> _mockMediator;
        private Mock<IHubContext<ChatHub>> _mockChat;
        private Mock<IQueueMessageService> _mockQueue;
        private IUserRepository _userRepository;
        private DomainNotificationHandler _domainNotificationHandler;
        private IMapper _mapper;
        private UserController _controller;

        [TestInitialize]
        public void InitTests()
        {
            db = GetDbInstance();
            _unitOfWork = new UnitOfWork(db);
            _mockChat = new Mock<IHubContext<ChatHub>>();
            _userRepository = new UserRepository(db);
            _mockMediator = new Mock<IMediatorHandler>();
            _domainNotificationHandler = new DomainNotificationHandler();
            _mockMediator.Setup(x => x.RaiseEvent(It.IsAny<DomainNotification>())).Callback<DomainNotification>((x) =>
            {
                _domainNotificationHandler.Handle(x, CancellationToken.None);
            });
            _mockQueue = new Mock<IQueueMessageService>();
            _mockQueue.Setup(x => x.SendMessageAsync(It.IsAny<MessageDto>())).Returns(Task.CompletedTask);

            _mapper = AutoMapperConfig.RegisterMappings().CreateMapper();
        }
        [TestMethod]
        public async Task Should_not_return_list_of_users()
        {
            _controller = new UserController(new UserService(_userRepository, _mapper), _mockChat.Object, _mockQueue.Object, _domainNotificationHandler, _mockMediator.Object); ;

            var result = (await _controller.Get() as OkObjectResult)?.Value as ApiOkReturn;
            var list = result?.Data as List<UserDto>;
            Assert.AreEqual(0, list?.Count);
        }

        [TestMethod]
        public async Task Should_return_list_of_users()
        {
            var user = new User
            {
                Email = "test.one@test.com",
                Password = "123456",
                Name = "test one"
            };
            var userTwo = new User
            {
                Email = "test.two@live.com",
                Password = "123456",
                Name = "test two"
            };

            _userRepository.Add(user);
            _userRepository.Add(userTwo);
            _unitOfWork.Commit();

            _controller = new UserController(new UserService(_userRepository, _mapper), _mockChat.Object, _mockQueue.Object, _domainNotificationHandler, _mockMediator.Object);

            var result = (await _controller.Get() as OkObjectResult)?.Value as ApiOkReturn;
            var list = result?.Data as List<UserDto>;

            
            Assert.AreEqual(2, list?.Count);
        }
        [TestMethod]
        public async Task Should_not_return_a_user()
        {
            _controller = new UserController(new UserService(_userRepository, _mapper), _mockChat.Object, _mockQueue.Object, _domainNotificationHandler, _mockMediator.Object);

            var result = (await _controller.Get(Guid.NewGuid()) as OkObjectResult)?.Value as ApiOkReturn;

            Assert.IsNull(result?.Data);
        }

        [TestMethod]
        public async Task Should_return_a_user()
        {
            var user = new User
            {
                Email = "test.one@test.com",
                Password = "123456",
                Name = "test one"
            };
            var userTwo = new User
            {
                Email = "test.two@live.com",
                Password = "123456",
                Name = "test two"
            };

            _userRepository.Add(user);
            _userRepository.Add(userTwo);
            _unitOfWork.Commit();

            _controller = new UserController(new UserService(_userRepository, _mapper), _mockChat.Object, _mockQueue.Object, _domainNotificationHandler, _mockMediator.Object);

            var result = (await _controller.Get(user.Id) as OkObjectResult)?.Value as ApiOkReturn;
            var getUser = result?.Data as UserDto;


            Assert.AreEqual(user.Id, getUser?.Id);
        }

        [TestMethod]
        public async Task Should_not_return_list_of_messages()
        {
            _controller = new UserController(new UserService(_userRepository, _mapper), _mockChat.Object, _mockQueue.Object, _domainNotificationHandler, _mockMediator.Object);

            var result = (await _controller.GetMessages() as OkObjectResult)?.Value as ApiOkReturn;
            var list = result?.Data as List<Messages>;
            Assert.AreEqual(0, list?.Count);
        }

        [TestMethod]
        public async Task Should_return_list_of_messages()
        {
            var message = new Messages() 
            { 
                Message = "Hello world one", Sender = "test@teste.com.br",  Consumer = "teste2@teste.com" ,
                Date = DateTime.Now
            };

            var messageTwo = new Messages()
            {
                Message = "Hello world Two",
                Sender = "test@teste.com.br",
                Consumer = "teste2@teste.com",
                Date = DateTime.Now
            };

            _userRepository.Add(message);
            _userRepository.Add(messageTwo);
            _unitOfWork.Commit();

            _controller = new UserController(new UserService(_userRepository, _mapper), _mockChat.Object, _mockQueue.Object, _domainNotificationHandler, _mockMediator.Object);

            var result = (await _controller.GetMessages() as OkObjectResult)?.Value as ApiOkReturn;
            var list = result?.Data as List<Messages>;

            Assert.AreEqual(2, list?.Count);
        }
    }
}
