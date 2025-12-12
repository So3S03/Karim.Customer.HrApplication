using Karim.Customer.HrApplication.Application.Abstraction.ManagerContract;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Department;
using Karim.Customer.HrApplication.Application.Services.Department;

namespace Karim.Customer.HrApplication.Application.Manager
{
    public class ServicesManager : IServicesManager
    {
        private readonly Lazy<IDepartmentService> _departmentService;

        public ServicesManager(
            Func<IDepartmentService> departmentServicesFactory
            )
        {
            _departmentService = new Lazy<IDepartmentService>(departmentServicesFactory, LazyThreadSafetyMode.ExecutionAndPublication);
        }
        public IDepartmentService DepartmentService => _departmentService.Value;

    }
}
