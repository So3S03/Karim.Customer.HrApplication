using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;

namespace Karim.Customer.HrApplication.Application.Specifications.Dashboard
{
    internal class EmpTerminatedSpecification(int year) : BaseSpecifications<employee, string>(E => E.TerminateResignedDate.Value.Year == year)
    {
    }
}
