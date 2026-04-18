using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Attendance;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Contracts;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Department;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Employee;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Identity;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Projects;

namespace Karim.Customer.HrApplication.Application.Abstraction.ManagerContract
{
    public interface IServicesManager
    {
        public IDepartmentService DepartmentService { get; }
        public IEmployeeService EmployeeService { get; }
        public IAuthServices AuthService { get; }
        public IAttendanceServices AttendanceService { get; }
        public IProjectServices ProjectService { get; }
        public IContractService ContractService { get; }
    }
}
