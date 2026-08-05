using Karim.Customer.HrApplication.Domain.Entities.Payroll;

namespace Karim.Customer.HrApplication.Application.Specifications.Payrolls
{
    internal class PayslipsPerMonthSpeciications : BaseSpecifications<Payslip, string>
    {
        public PayslipsPerMonthSpeciications() : base(P => P.CreatedOn.Month == DateTime.Now.Month && P.EmployeeType != Domain.Entities.Employee.EmployeeType.Freelance)
        {
            
        }
    }
}
