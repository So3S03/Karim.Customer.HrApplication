using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;
namespace Karim.Customer.HrApplication.Application.Specifications.Payrolls
{
    internal class NotTerminatedOrResignedEmployees :BaseSpecifications<employee, string>
    {
        public NotTerminatedOrResignedEmployees(): base(E => (E.EmployeeStatus != Domain.Entities.Employee.EmployeeStatus.Terminated ||
        E.EmployeeStatus != Domain.Entities.Employee.EmployeeStatus.Resigned) &&
        E.IsHasContract == true)
        {
            AddInclude(E => E.FingerprintLog!);
            AddInclude(E => E.Requests!);
        }
    }
}
