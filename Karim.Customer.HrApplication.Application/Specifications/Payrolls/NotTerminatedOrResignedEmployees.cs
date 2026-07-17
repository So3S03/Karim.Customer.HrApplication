using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;
namespace Karim.Customer.HrApplication.Application.Specifications.Payrolls
{
    internal class NotTerminatedOrResignedEmployees :BaseSpecifications<employee, string>
    {
        public NotTerminatedOrResignedEmployees(): base(E => E.EmployeeStatus != Domain.Entities.Employee.EmployeeStatus.Terminated &&
        E.EmployeeStatus != Domain.Entities.Employee.EmployeeStatus.Resigned &&
        E.IsHasContract == true)
        {
            var firstDayOfMonth = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            AddInclude(E => E.FingerprintLog!.Where(FP => FP.Date >= firstDayOfMonth && FP.Date <= lastDayOfMonth));
            AddInclude(E => E.Requests!.Where(FP => FP.StartDate >= firstDayOfMonth && FP.EndDate <= lastDayOfMonth));
        }
    }
}
