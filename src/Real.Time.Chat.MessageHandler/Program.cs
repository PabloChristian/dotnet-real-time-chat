using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.Shared.Kernel.Handler;
using Real.Time.Chat.Infrastructure.Security;
using Real.Time.Chat.MessageHandler.Configurations;
using Real.Time.Chat.MessageHandler.Contracts;
using Real.Time.Chat.MessageHandler.Contracts.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real.Time.Chat.MessageBroker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    Console.WriteLine(hostingContext.HostingEnvironment.EnvironmentName);
                    config
                        .SetBasePath(Environment.CurrentDirectory)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true);

                    config.AddEnvironmentVariables();
                })
                .ConfigureLogging((c, l) =>
                {
                    l.AddConfiguration(c.Configuration);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    
                    var configuration = hostContext.Configuration;
                    services.AddScoped<LoggingHttpHandler>();
                    services.AddScoped<IChatService, ChatService>();
                    services.AddScoped<IDeliveryMessageRequest, DeliveryMessageRequest>();
                    var rabbitMqOptions = configuration.GetSection("RabbitMqConfig").Get<RabbitMqOptions>();
                    services.AddRabbitMq(rabbitMqOptions);
                    services.AddIdentitySetup(configuration);
                    services.AddHttpClient("financialchat", c =>
                    {
                        c.Timeout = TimeSpan.FromSeconds(5);
                        c.BaseAddress = new Uri(configuration.GetValue<string>("ApiUrl"));
                    });
                });

        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Environment.Exit(1);
        }
    }
}
