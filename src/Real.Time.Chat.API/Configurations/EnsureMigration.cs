using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Real.Time.Chat.Infrastructure.Data;

namespace Real.Time.Chat.Api.Configurationsurations
{
    public static class EnsureMigration
    {
        public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<T>();

            if(dbContext != null) 
                dbContext.Database.Migrate();
        }
    }
}