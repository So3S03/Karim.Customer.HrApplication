using Karim.Customer.HrApplication.Application.Abstraction.ManagerContract;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Department;
using Karim.Customer.HrApplication.Application.AppAssembly;
using Karim.Customer.HrApplication.Application.Manager;
using Karim.Customer.HrApplication.Application.MapsterConfigurations;
using Karim.Customer.HrApplication.Application.Services.Department;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Karim.Customer.HrApplication.Application.ApplicationDI
{
    public static class ApplicationDependancyInjection
    {
        public static IServiceCollection ApplicationDIContainer(this IServiceCollection services)
        {
            //registering mapster
            //services.AddMapster();
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(ApplicationAssembly).Assembly);
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            services.AddSingleton<FilesPathResolver>();

           //registering Department services
            services.AddScoped(typeof(IDepartmentService), typeof(DepartmentServices));
            services.AddScoped<Func<IDepartmentService>>(sp =>
            {
                return () => sp.GetRequiredService<IDepartmentService>();
            });


            services.AddScoped(typeof(IServicesManager), typeof(ServicesManager));
            return services;
        }
    }
}
