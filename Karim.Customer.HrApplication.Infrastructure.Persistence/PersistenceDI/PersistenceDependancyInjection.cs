using Karim.Customer.HrApplication.Domain.Entities.Identity;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Contexts;
using Karim.Customer.HrApplication.Infrastructure.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.PersistenceDI
{
    public static class PersistenceDependancyInjection
    {
        public static IServiceCollection AddPersistenceDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HRMSDBContext>(optionAction =>
            {
                optionAction.UseSqlServer(configuration.GetConnectionString("HRMSContext"));
            });

            services.AddIdentity<AppUser, AppPrivilages>(
                (identityConfigs) =>
                {
                    //Configure SignIn Options
                    identityConfigs.SignIn.RequireConfirmedPhoneNumber = true;

                    //Configure Password Options
                    identityConfigs.Password.RequiredLength = 8;
                    identityConfigs.Password.RequireNonAlphanumeric = true;
                    identityConfigs.Password.RequireDigit = true;
                    identityConfigs.Password.RequireUppercase = true;

                    //Configure Lockout Options
                    identityConfigs.Lockout.MaxFailedAccessAttempts = 5;
                    identityConfigs.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    identityConfigs.Lockout.AllowedForNewUsers = true;

                    //Configure
                    identityConfigs.User.RequireUniqueEmail = true;
                    identityConfigs.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                }
                )
                .AddEntityFrameworkStores<HRMSDBContext>();

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork<HRMSDBContext>));
            return services;
        }
    }
}
