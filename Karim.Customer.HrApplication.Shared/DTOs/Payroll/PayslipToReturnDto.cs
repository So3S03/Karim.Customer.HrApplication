namespace Karim.Customer.HrApplication.Shared.DTOs.Payroll
{
    public class PayslipToReturnDto
    {
        public required string Id { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public required decimal BasicSalary { get; set; }
        public decimal? TotalOvertime { get; set; }
        public decimal NetSalary { get; set; }
        public int StatusId { get; set; }
        public string? Status { get; set; }
        public DateTime? PaidAt { get; set; }
        public int? PaymentWayId { get; set; }
        public string? PaymentWay { get; set; }
        public string? PaidNotes { get; set; }

        //Relations
        //Employee
        public required string EmployeeName { get; set; }
        public required string EmployeeCode { get; set; }
        public required string EmployeeId { get; set; }
        //PayrollBonus
        public List<PayrollBonusToReturnDto>? PayrollBonuses { get; set; }
        //PayrollAllowance
        public List<PayrollAllowanceToReturnDto>? PayrollAllowances { get; set; }
        //PayrollPenalty
        public List<PayrollPenaltyToReturnDto>? PayrollPenalties { get; set; }
    }
}
