using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Real.Time.Chat.Api.Configurations
{
    public static class MigrationConfiguration
    {
        public static void AddMigration<T>(this IApplicationBuilder app) where T : Real.Time.Chat.Infrastructure.Data.Context.RealTimeChatContext
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<T>();
            dbContext?.Database.Migrate();
        }
    }
}