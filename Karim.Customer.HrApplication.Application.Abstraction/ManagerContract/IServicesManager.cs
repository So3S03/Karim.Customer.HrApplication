using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Attendance;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Department;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Employee;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Identity;

namespace Karim.Customer.HrApplication.Application.Abstraction.ManagerContract
{
    public interface IServicesManager
    {
        public IDepartmentService DepartmentService { get; }
        public IEmployeeService EmployeeService { get; }
        public IAuthServices AuthService { get; }
        public IAttendanceServices AttendanceService { get; }
    }
}
