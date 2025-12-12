using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Department;

namespace Karim.Customer.HrApplication.Application.Abstraction.ManagerContract
{
    public interface IServicesManager
    {
        public IDepartmentService DepartmentService { get; }
    }
}
