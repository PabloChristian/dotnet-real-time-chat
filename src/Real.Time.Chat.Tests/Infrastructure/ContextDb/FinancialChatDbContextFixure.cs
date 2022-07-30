using Real.Time.Chat.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Real.Time.Chat.Tests.ContextDb
{
    public class RealTimeChatDbContextFixure
    {
        protected RealTimeChatContext db;

        protected static DbContextOptions<RealTimeChatContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<RealTimeChatContext>();
            builder.UseInMemoryDatabase("RealTimeDbTest")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected static RealTimeChatContext GetDbInstance() => new(CreateNewContextOptions());
    }
}
