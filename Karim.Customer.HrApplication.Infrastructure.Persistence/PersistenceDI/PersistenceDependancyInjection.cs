using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Contexts;
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
            return services;
        }
    }
}
