﻿using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.MessageBus.Consumers;
using MassTransit;

namespace Real.Time.Chat.MessageBus.Configurations
{
    public static class RabbitMqConfiguration
    {
        public static void AddRabbitMq(this IServiceCollection services, RabbitMqOptions rabbitMqOptions)
        {
            services.AddMassTransit(options =>
            {
                options.AddConsumer<CaptureMessageConsumer>();
                options.AddConsumer<CaptureMessageFaultConsumer>();

                options.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"{rabbitMqOptions.Url}"), h =>
                    {
                        h.Username(rabbitMqOptions.Username);
                        h.Password(rabbitMqOptions.Password);
                    });
                    
                    cfg.ReceiveEndpoint(rabbitMqOptions.Queue, e =>
                    {
                        e.UseScheduledRedelivery(r => r.Intervals(new TimeSpan[] { TimeSpan.FromSeconds(15), TimeSpan.FromMinutes(1) }));

                        e.DiscardFaultedMessages();

                        e.ConfigureConsumer<CaptureMessageConsumer>(context);
                        e.ConfigureConsumer<CaptureMessageFaultConsumer>(context);
                    });
                });
            });
        }
    }
}
