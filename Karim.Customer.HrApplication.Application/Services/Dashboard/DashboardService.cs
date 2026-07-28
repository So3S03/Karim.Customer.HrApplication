using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Dashboard;
using employee =  Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.Dashboard;
using department = Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;




using Karim.Customer.HrApplication.Domain.Entities.Projects;
using Karim.Customer.HrApplication.Domain.Entities.Tasks;
using Karim.Customer.HrApplication.Application.Specifications.Dashboard;
using Karim.Customer.HrApplication.Domain.Entities.Payroll;
using Karim.Customer.HrApplication.Domain.Entities.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Karim.Customer.HrApplication.Domain.Entities.Attendance;
using Karim.Customer.HrApplication.Application.Specifications.Employee;

namespace Karim.Customer.HrApplication.Application.Services.Dashboard
{
    internal class DashboardService(IUnitOfWork _unitOfWork) : IDashboardService
    {
        public async Task<CompanyStatusToReturnDto> GetCompanyStatusDto()
        {
            //Create Repos
            var EmployeeRepo = _unitOfWork.GenerateRepository<employee, string>();
            var DepartmentRepo = _unitOfWork.GenerateRepository<department, string>();
            var ProjectRepo = _unitOfWork.GenerateRepository<Project, string>();
            var TaskRepo = _unitOfWork.GenerateRepository<Tasks, string>();
            var ContractRepo = _unitOfWork.GenerateRepository<Contract, string>();
            var PayrollRepo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Get Count Of Employees On Database
            var EmployeeCount = await EmployeeRepo.GetDataCountAsync(null);
            //Create Spec For Getting Count Of New Hire This Month
            var NewHireSpec = new NewHireThisMonthSpecification();
            //Get Count Of New Hires
            var NewHiresCount = await EmployeeRepo.GetDataCountAsync(NewHireSpec);
            //Get Departments Count
            var DepartmentCount = await DepartmentRepo.GetDataCountAsync(null);
            //Get Projects Count
            var ProjectsCount = await ProjectRepo.GetDataCountAsync(null);
            //Create Spec For Active Projects
            var ActiveProjectsSpec = new ActiveProjectsSpecification();
            //Get Active Projects
            var ActiveProjectsCount = await ProjectRepo.GetDataCountAsync(ActiveProjectsSpec);
            //Get Active Projects Budget Sum
            var ActiveProjectBudgetSum = await ProjectRepo.GetDataSumAsync(ActiveProjectsSpec, P => P.ProjectCoast);
            //Get Total Tasks
            var TasksCount = await TaskRepo.GetDataCountAsync(null);
            //Create Spec For Retrive Total Payrolls Per Last Month
            var PayrollLastMonthSpec = new TotalPayrollPerMonthSpecification(-1);
            //Get Total Payroll Value Per This Month
            var LastMonthPayrollSum = await PayrollRepo.GetDataSumAsync(PayrollLastMonthSpec, P => P.NetSalary);
            //Create Spec For Retrive Total Payrolls Per Current Month
            var CurrentMonthSpec = new TotalPayrollPerMonthSpecification(0);
            //Get Sum Of Current Month Payrolls
            var CurrentMonthPayrollSum = await PayrollRepo.GetDataSumAsync(CurrentMonthSpec, P => P.NetSalary);
            //Create Spec For Getting Expired Employee Contracts
            var EmployeeContractSpec = new ExpiredContractsSpecifications(ContractType.Employee);
            //Get Count Of Expired Contruct
            var ExpiredEmployeeContractCount = await ContractRepo.GetDataCountAsync(EmployeeContractSpec);
            //Create Spec For Getting Expired Projects Contracts
            var ProjectContractSpec = new ExpiredContractsSpecifications(ContractType.Project);
            //Get Count Of Expired Contruct
            var ExpiredProjectsContractCount = await ContractRepo.GetDataCountAsync(ProjectContractSpec);
            return new CompanyStatusToReturnDto()
            {
                TotalEmployees = EmployeeCount,
                NewHires = NewHiresCount,
                TotalProjects = ProjectsCount,
                TotalActiveProjects = ActiveProjectsCount,
                TotalDepartments = DepartmentCount,
                TotalTasks = TasksCount,
                TotalActiveProjectsBudgets = ActiveProjectBudgetSum,
                PastMonthTotalPayrollValue = LastMonthPayrollSum,
                CurrentMonthTotalPayrollValue = CurrentMonthPayrollSum,
                TotalExpiredEmployeeContracts = ExpiredEmployeeContractCount,
                TotalExpiredProjectsContracts = ExpiredProjectsContractCount
            };
        }
        public async Task<ICollection<PayrollComparisonPerMonthDto>> GetMonthlyPayrollsSumComparison(int? year)
        {
            //Check On Year
            if (year is null) year = DateTime.Now.Year;
            //Create Repo
            var PayrollRepo = _unitOfWork.GenerateRepository<Payslip, string>();
            //Create Spec
            var PayrollSpec = new PayrollsPeYearChartSpecification(year.Value);
            //Create Query With Grouping For Data Per Months
            var groupedData = await PayrollRepo.GetQuery(PayrollSpec)
                .GroupBy(P => P.StartDate.Month)
                .Select(G => new
                {
                    MonthNumber = G.Key,
                    MonthTotalSalary = G.Sum(S => S.NetSalary)
                }).ToListAsync();
            //Create Dictionary For Grouped Data
            var dictionaryData = groupedData.ToDictionary(x => x.MonthNumber, x => x.MonthTotalSalary);
            //Looping And Retrive Data
            var result = Enumerable.Range(1, 12).Select(X => new PayrollComparisonPerMonthDto()
            {
                MonthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(X),
                MonthTotalSalary = dictionaryData.TryGetValue(X, out decimal salary) ? salary : 0,
            }).ToList();
            return result;
        }
        public async Task<ICollection<AllEmployeesAttendanceRatePerMonthDto>> GetAttendanceRatePerMonthComparison(int? year)
        {
            //Check On Year
            if (year is null) year = DateTime.Now.Year;
            //Create Fingerprint Repo
            var FingerPrintRepo = _unitOfWork.GenerateRepository<Fingerprint, string>();
            //Create Spec
            var FingerPrintSpec = new AllFingerPrintsPerYearSpecification(year.Value);
            //Get All FingerPrints
            var GroupedData = await FingerPrintRepo.GetQuery(FingerPrintSpec)
                .GroupBy(FP => FP.Date.Month).Select(X => new
                {
                    MonthNumber = X.Key,
                    CountOfFPThisMonth = X.Count()
                }).ToListAsync();
            //Save Data Into Dictionary
            var DictionaryData = GroupedData.ToDictionary(x => x.MonthNumber, x => x.CountOfFPThisMonth);
            //Create EmpRepo
            var EmpRepo = _unitOfWork.GenerateRepository<employee, string>();
            //Create Spec
            var EmpSpec = new AllNotTerminatedOrRisignedEmployees();
            //Get Count Of Employees
            var EmpsCount = await EmpRepo.GetDataCountAsync(EmpSpec);
            //Create Result
            var result = Enumerable.Range(1, 12).Select(X =>
            {
                var daysInMonth = DateTime.DaysInMonth(year.Value, X);
                var workingDays = 0;
                for(int day = 1; day <= daysInMonth; day++)
                {
                    var date = new DateOnly(year.Value, X, day);
                    if(date.DayOfWeek != DayOfWeek.Friday && date.DayOfWeek != DayOfWeek.Saturday) workingDays++;
                }
                var excpectedAttendacne = EmpsCount * workingDays;
                DictionaryData.TryGetValue(X, out int actualAttendacne);
                decimal attendanceRate = excpectedAttendacne == 0 ? 0 : Math.Round(((decimal)actualAttendacne / excpectedAttendacne) * 100, 2);
                return new AllEmployeesAttendanceRatePerMonthDto()
                {
                    Month = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(X),
                    AttendanceRate = attendanceRate,
                };
            }).ToList();
            return result;
        }
    }
}
