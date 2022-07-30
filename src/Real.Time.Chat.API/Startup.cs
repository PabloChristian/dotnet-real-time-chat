using MediatR;
using Microsoft.EntityFrameworkCore;
using Real.Time.Chat.Api.Configurationsurations;
using Real.Time.Chat.Application.AutoMapper;
using Real.Time.Chat.Infrastructure.Data.Context;
using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.Api.Configurations;
using Real.Time.Chat.Infrastructure.Security;

namespace Real.Time.Chat.Web.API
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
            services.AddSwaggerSetup();
            services.AddSingleton(AutoMapperConfig.RegisterMappings().CreateMapper());
            services.AddMvc();
            services.AddLogging();
            services.AddHttpClient("RealTimeChat", cfg => { cfg.Timeout = TimeSpan.FromSeconds(60); });
            services.AddHttpContextAccessor();
            services.AddMediatR(typeof(Startup));
            services.Configure<RabbitMqOptions>(options => Configuration.GetSection("RabbitMqConfig").Bind(options));
            services.AddMassTransitSetup(Configuration.GetSection("RabbitMqConfig").Get<RabbitMqOptions>());

            //DependencyInjectionResolver.RegisterServices(services);

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

            app.UseGlobalExceptionMiddleware();
            app.UseSwaggerSetup();

            app.UseRouting();

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.EnsureMigrationOfContext<RealTimeChatContext>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<Application.SignalR.ChatHub>("/chatHub", options =>
                {
                    options.TransportMaxBufferSize = 36000;
                    options.ApplicationMaxBufferSize = 36000;
                    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.LongPolling;
                });
            });
        }
    }
}
