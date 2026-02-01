using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Department;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Employee;

namespace Karim.Customer.HrApplication.Application.Abstraction.ManagerContract
{
    public interface IServicesManager
    {
        public IDepartmentService DepartmentService { get; }
        public IEmployeeService EmployeeService { get; }
    }
}
