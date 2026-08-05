using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.Entities.Employee;

namespace Karim.Customer.HrApplication.Domain.Entities.Payroll
{
    public class Payslip : BaseAuditableEntity<string>
    {
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public required decimal BasicSalary { get; set; }
        public decimal? TotalOvertime { get; set; }
        public decimal NetSalary { get; set; }
        public PayrollStatus Status { get; set; }
        public DateTime? PaidAt { get; set; }
        public PayrollPaymentWay? PaymentWay { get; set; }
        public string? PaidNotes { get; set; }
        public EmployeeType EmployeeType { get; set; }

        //Relations
        //Employee
        public required Employee.Employee Employee { get; set; }
        public required string EmployeeId { get; set; }
        //PayrollBonus
        public List<PayrollBonus>? PayrollBonuses { get; set; }
        //PayrollAllowance
        public List<PayrollAllowance>? PayrollAllowances { get; set; }
        //PayrollPenalty
        public List<PayrollPenalty>? PayrollPenalties { get; set; }
    }
}
