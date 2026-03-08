using Karim.Customer.HrApplication.Domain.Entities.Identity;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Contexts;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Privilages;
using Microsoft.AspNetCore.Identity;
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
                //Seed Privs And Admin User
                if(typeof(DbType) == typeof(HRMSDBContext))
                {
                    var hrdb = db as HRMSDBContext;
                    var privs = PrivList.GeneratePrivilages();
                    var existingDataCount = await db.Set<AppPrivilages>().Select(P => P.Name).ToListAsync();
                    if(existingDataCount.Count == 0 || existingDataCount.Count != privs.Count)
                    {
                        var newPrivs = privs.Where(p => existingDataCount.Contains(p.Name) == false).ToList();
                        if(newPrivs.Count > 0)
                        {
                            await hrdb!.Set<AppPrivilages>().AddRangeAsync(newPrivs);
                            var saved = await hrdb!.SaveChangesAsync();
                            if (saved == 0) throw new Exception("Something Went Wrong While seeding Privs!");
                        }
                    }
                    var usersExist = hrdb!.Set<AppUser>().Count() > 0;
                    if(!usersExist)
                    {
                        var userManagerServices = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                        var user = new AppUser()
                        {
                            Id = Guid.NewGuid().ToString(),
                            DisplayName = "Admin",
                            UserName = "Admin",
                            AccessFailedCount = 0,
                            Email = "Admin@HRMS.com",
                            EmailConfirmed = true,
                            isSuspended = false,
                            NormalizedEmail = "ADMIN@HRMS.COM",
                            isRemoved = false,
                            NormalizedUserName = "ADMIN",
                            PhoneNumber = "0000000000",
                            PhoneNumberConfirmed = true,
                            CreatedBy = "0",
                            CreatedOn = DateTime.UtcNow,
                            LockoutEnabled = false
                        };
                        var addedUser = await userManagerServices.CreateAsync(user, "@Admin@2026");
                        if (!addedUser.Succeeded) throw new Exception("Something Went Wrong While Adding User");
                        var addedRole = await userManagerServices.AddToRoleAsync(user, "Admin");
                        if (!addedRole.Succeeded) throw new Exception("Something Went Wrong While Assign The Privilage");
                    }
                }
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, ex.Message);
            }
             
        }
    }
}
