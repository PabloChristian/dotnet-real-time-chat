using MediatR;
using Microsoft.EntityFrameworkCore;
using Real.Time.Chat.Api.Configurations;
using Real.Time.Chat.Application.AutoMapper;
using Real.Time.Chat.Infrastructure.Data.Context;
using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.Infrastructure.Security;
using Real.Time.Chat.Infrastructure.InversionOfControl;

namespace Real.Time.Chat.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder => builder.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials().Build());
            });

            services.AddDbContext<RealTimeChatContext>(options => options.UseSqlServer(Configuration.GetConnectionString("RealTimeChatConnection")));
            services.AddIdentitySetup(Configuration);
            AutoMapperConfig.RegisterMappings();
            services.AddSwagger();
            services.AddSingleton(AutoMapperConfig.RegisterMappings().CreateMapper());
            services.AddMvc();
            services.AddLogging();
            services.AddHttpClient("RealTimeChat", cfg => { cfg.Timeout = TimeSpan.FromSeconds(60); });
            services.AddHttpContextAccessor();
            services.AddMediatR(typeof(Startup));
            services.Configure<RabbitMqOptions>(options => Configuration.GetSection("RabbitMqConfig").Bind(options));
            services.AddMassTransit(Configuration.GetSection("RabbitMqConfig").Get<RabbitMqOptions>());

            services.RegisterServices();

            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
                hubOptions.ClientTimeoutInterval = TimeSpan.FromSeconds(15);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.AddMiddlewares();
            app.UseSwaggerSetup();

            app.UseRouting();

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.AddMigration<RealTimeChatContext>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<Application.SignalR.MessageChatHub>("/chatHub", options =>
                {
                    options.TransportMaxBufferSize = 36000;
                    options.ApplicationMaxBufferSize = 36000;
                    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.LongPolling;
                });
            });
        }
    }
}
