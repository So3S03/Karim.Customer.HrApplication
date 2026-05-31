using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.Entities.Employee;

namespace Karim.Customer.HrApplication.Domain.Entities.Payroll
{
    public class Payslip : BaseAuditableEntity<string>
    {
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal TotalAllowances { get; set; }
        public decimal TotalOvertime { get; set; }
        public decimal TotalBonuses { get; set; }
        public DateTime? BonusAt { get; set; }
        public decimal TotalDeductions { get; set; }
        public DateTime? DeductionAt { get; set; }
        public decimal TotalTax { get; set; }
        public decimal NetSalary { get; set; }
        public PayrollStatus Status { get; set; }
        public DateTime? PaidAt { get; set; }
        public string? PaidNotes { get; set; }


        public required Employee.Employee Employee { get; set; }
        public required string EmployeeId { get; set; }
    }
}
