using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;

namespace Karim.Customer.HrApplication.Application.Specifications.Dashboard
{
    internal class AllNotTerminatedOrRisignedEmployees : BaseSpecifications<employee, string>
    {
        public AllNotTerminatedOrRisignedEmployees(): base(E => E.EmployeeStatus != Domain.Entities.Employee.EmployeeStatus.Resigned && E.EmployeeStatus != Domain.Entities.Employee.EmployeeStatus.Terminated)
        {
            
        }
    }
}
