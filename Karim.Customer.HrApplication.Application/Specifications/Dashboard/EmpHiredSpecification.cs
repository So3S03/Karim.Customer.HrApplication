using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;
namespace Karim.Customer.HrApplication.Application.Specifications.Dashboard
{
    internal class EmpHiredSpecification(int year) : BaseSpecifications<employee, string>(E => E.JoinDate.Year == year)
    {
    }
}
