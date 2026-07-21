using Karim.Customer.HrApplication.Application.Abstraction.ManagerContract;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Attendance;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Contracts;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Department;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Employee;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Identity;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Payrolls;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Projects;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Task;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Tickets;
using Karim.Customer.HrApplication.Application.AppAssembly;
using Karim.Customer.HrApplication.Application.Manager;
using Karim.Customer.HrApplication.Application.MapsterConfigurations;
using Karim.Customer.HrApplication.Application.Services.Attendance;
using Karim.Customer.HrApplication.Application.Services.Contracts;
using Karim.Customer.HrApplication.Application.Services.Department;
using Karim.Customer.HrApplication.Application.Services.Employee;
using Karim.Customer.HrApplication.Application.Services.Identity;
using Karim.Customer.HrApplication.Application.Services.Payrolls;
using Karim.Customer.HrApplication.Application.Services.Projects;
using Karim.Customer.HrApplication.Application.Services.Task;
using Karim.Customer.HrApplication.Application.Services.Ticket;
using Karim.Customer.HrApplication.Domain.Entities.Identity;
using Karim.Customer.HrApplication.Shared._Common.Interfaces;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
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
            //registering Employee Services
            services.AddScoped(typeof(IEmployeeService), typeof(EmployeeService));
            services.AddScoped<Func<IEmployeeService>>(serviceProvider =>
            {
                return () => serviceProvider.GetRequiredService<IEmployeeService>();
            });
            //registering Auth Services
            services.AddScoped(typeof(IAuthServices), typeof(AuthServices));
            services.AddScoped<Func<IAuthServices>>((serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<IAuthServices>();
            });

            services.AddScoped(typeof(IAttendanceServices), typeof(AttendanceServices));
            services.AddScoped<Func<IAttendanceServices>>((serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<IAttendanceServices>();
            });

            services.AddScoped(typeof(IProjectServices), typeof(ProjectServices));
            services.AddScoped<Func<IProjectServices>>((serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<IProjectServices>();
            });

            services.AddScoped(typeof(IContractService), typeof(ContractService));
            services.AddScoped<Func<IContractService>>(serviceProvider =>
            {
                return () => serviceProvider.GetRequiredService<IContractService>();
            });

            services.AddScoped(typeof(ITicketServices), typeof(TicketService));
            services.AddScoped<Func<ITicketServices>>(serviceProvider =>
            {
                return () => serviceProvider.GetRequiredService<ITicketServices>();
            });

            services.AddScoped(typeof(ITaskService), typeof(TaskService));
            services.AddScoped<Func<ITaskService>>(serviceProvider =>
            {
                return () => serviceProvider.GetRequiredService<ITaskService>();
            });

            services.AddScoped(typeof(IPayrollService), typeof(PayrollService));
            services.AddScoped<Func<IPayrollService>>(serviceProvider =>
            {
                return () => serviceProvider.GetRequiredService<IPayrollService>();
            });

            services.AddHttpContextAccessor();
            services.AddScoped<ILoggedInUserService, LoggedInUserService>();

            services.AddScoped(typeof(IServicesManager), typeof(ServicesManager));
            return services;
        }
    }
}
