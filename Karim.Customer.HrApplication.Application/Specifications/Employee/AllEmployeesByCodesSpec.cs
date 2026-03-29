using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;

namespace Karim.Customer.HrApplication.Application.Specifications.Employee
{
    internal class AllEmployeesByCodesSpec(ICollection<string> codes) : BaseSpecifications<employee, string>(E => codes.Contains(E.EmployeeCode))
    {
    }
}
