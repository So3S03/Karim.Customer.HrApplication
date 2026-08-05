using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;
namespace Karim.Customer.HrApplication.Application.Specifications.Dashboard
{
    internal class NewHireThisMonthSpecification : BaseSpecifications<employee, string>
    {
        public NewHireThisMonthSpecification() : base(E => E.JoinDate.Month == DateTime.Now.Month)
        {
            
        }
    }
}
