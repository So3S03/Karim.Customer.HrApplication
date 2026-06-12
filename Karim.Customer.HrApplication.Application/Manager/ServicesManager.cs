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
using Karim.Customer.HrApplication.Application.Services.Department;

namespace Karim.Customer.HrApplication.Application.Manager
{
    public class ServicesManager : IServicesManager
    {
        private readonly Lazy<IDepartmentService> _departmentService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IAuthServices> _authService;
        private readonly Lazy<IAttendanceServices> _attendanceService;
        private readonly Lazy<IProjectServices> _projectServices;
        private readonly Lazy<IContractService> _contractService;
        private readonly Lazy<ITicketServices> _ticketServices;
        private readonly Lazy<ITaskService> _taskService;
        private readonly Lazy<IPayrollService> _payrollService;

        public ServicesManager(
            Func<IDepartmentService> departmentServicesFactory,
            Func<IEmployeeService> employeeServiceFactory,
            Func<IAuthServices> authServiceFactory,
            Func<IAttendanceServices> attendanceFactory,
            Func<IProjectServices> projectFactory,
            Func<IContractService> contractFactory,
            Func<ITicketServices> ticketFactory,
            Func<ITaskService> taskService,
            Func<IPayrollService> payrollService
            )
        {
            _departmentService = new Lazy<IDepartmentService>(departmentServicesFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _employeeService = new Lazy<IEmployeeService>(employeeServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _authService = new Lazy<IAuthServices>(authServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _attendanceService = new Lazy<IAttendanceServices>(attendanceFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _projectServices = new Lazy<IProjectServices>(projectFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _contractService = new Lazy<IContractService>(contractFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _ticketServices = new Lazy<ITicketServices>(ticketFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _taskService = new Lazy<ITaskService>(taskService, LazyThreadSafetyMode.ExecutionAndPublication);
            _payrollService = new Lazy<IPayrollService>(payrollService, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public IDepartmentService DepartmentService => _departmentService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;
        public IAuthServices AuthService => _authService.Value;
        public IAttendanceServices AttendanceService => _attendanceService.Value;
        public IProjectServices ProjectService => _projectServices.Value;
        public IContractService ContractService => _contractService.Value;
        public ITicketServices TicketServices => _ticketServices.Value;
        public ITaskService TaskService => _taskService.Value;
        public IPayrollService PayrollService => _payrollService.Value;
    }
}
