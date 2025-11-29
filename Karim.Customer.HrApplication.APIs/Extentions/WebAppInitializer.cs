using Microsoft.EntityFrameworkCore;

namespace Karim.Customer.HrApplication.APIs.Extentions
{
    public static class WebAppInitializer
    {
        public static async Task DbMigrate<DbType>(this IApplicationBuilder app)
            where DbType : DbContext
        {
            using var scope = app.ApplicationServices.CreateScope(); //Why Creating Scope [Answer in next line]
            var db = scope.ServiceProvider.GetRequiredService<DbType>(); //To ASK CLR for an object from class that inhirete from DbContext to start operation on database
            var LoggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>(); //To ASK CLR for an object from class that impliment ILoggerFactory to start logging on exceptions
            try
            {
                var pendingMigrations = await db.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any()) await db.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, ex.Message);
            }
             
        }
    }
}
