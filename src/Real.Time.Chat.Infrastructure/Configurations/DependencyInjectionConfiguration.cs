using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Real.Time.Chat.Application.Services;
using Real.Time.Chat.Domain.CommandHandlers;
using Real.Time.Chat.Domain.Commands;
using Real.Time.Chat.Domain.Commands.Message;
using Real.Time.Chat.Domain.Interfaces;
using Real.Time.Chat.Domain.Interfaces.Messaging;
using Real.Time.Chat.Domain.Interfaces.Services;
using Real.Time.Chat.Infrastructure.Data;
using Real.Time.Chat.Infrastructure.Data.Context;
using Real.Time.Chat.Infrastructure.Data.Repositories;
using Real.Time.Chat.Infrastructure.ServiceBus;
using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.Shared.Kernel.Handler;
using Real.Time.Chat.Shared.Kernel.Notifications;

namespace Real.Time.Chat.Infrastructure.InversionOfControl
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddDbContext<RealTimeChatContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRequestHandler<UserAddCommand, bool>, UserHandler>();
            services.AddScoped<IRequestHandler<MessageAddCommand, bool>, UserHandler>();
            services.AddScoped<IRequestHandler<AuthenticateUserCommand, TokenJWT>, IdentityHandler>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoginService, IdentityService>();
            services.AddScoped<IQueueMessageService, QueueMessageService>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        }
    }
}
