using Hangfire;
using Karim.Customer.HrApplication.Domain.Conttracts;
using Karim.Customer.HrApplication.Infrastructure.ExcelSheetServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Karim.Customer.HrApplication.Infrastructure.InfraDIContainer
{
    public static class InfrastructureDIContainer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //Add ExcelSheet Services
            services.AddScoped(typeof(IExcelServices), typeof(ExcelServices));
            services.AddHangfire((config) =>
            {
                config.UseSqlServerStorage(configuration.GetConnectionString("HRMSContext"));
            });
            services.AddHangfireServer();
            return services;
        }
    }
}
