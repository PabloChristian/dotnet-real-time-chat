using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Real.Time.Chat.API.Configurations
{
    public static class EnsureMigration
    {
        public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : Infra.Data.Context.RealTimeChatContext
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<T>();

            if(dbContext != null) 
                dbContext.Database.Migrate();
        }
    }
}