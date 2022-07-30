using MassTransit;
using Real.Time.Chat.Shared.Kernel.Entity;

namespace Real.Time.Chat.Api.Configurationsurations
{
    public static class MassTransitConfiguration
    {
        public static void AddMassTransit(this IServiceCollection services, RabbitMqOptions options)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(options.Url), h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });
            });

            services.AddSingleton(busControl);
        }
    }
}
