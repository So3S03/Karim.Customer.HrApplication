using Karim.Customer.HrApplication.Application.Abstraction.ManagerContract;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Department;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Employee;
using Karim.Customer.HrApplication.Application.Services.Department;

namespace Karim.Customer.HrApplication.Application.Manager
{
    public class ServicesManager : IServicesManager
    {
        private readonly Lazy<IDepartmentService> _departmentService;
        private readonly Lazy<IEmployeeService> _employeeService;

        public ServicesManager(
            Func<IDepartmentService> departmentServicesFactory,
            Func<IEmployeeService> employeeServiceFactory
            )
        {
            _departmentService = new Lazy<IDepartmentService>(departmentServicesFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _employeeService = new Lazy<IEmployeeService>(employeeServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);
        }
        public IDepartmentService DepartmentService => _departmentService.Value;

        public IEmployeeService EmployeeService => _employeeService.Value;
    }
}
