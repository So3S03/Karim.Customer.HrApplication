using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;

namespace Karim.Customer.HrApplication.Domain.Entities.Payroll
{
    public class PayrollAllowance : BaseAuditableEntity<string>
    {
        public required string Title { get; set; }
        public required decimal Value { get; set; }
        public string? Description { get; set; }

        //Relations
        //Payslip
        public required Payslip Payslip { get; set; }
        public required string PayslipId { get; set; }
    }
}
