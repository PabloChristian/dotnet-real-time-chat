using MassTransit;
using Real.Time.Chat.Shared.Kernel.Entity;

namespace Real.Time.Chat.Api.Configurationsurations
{
    public static class MassTransitSetup
    {
        public static void AddMassTransitSetup(this IServiceCollection services, RabbitMqOptions options)
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
