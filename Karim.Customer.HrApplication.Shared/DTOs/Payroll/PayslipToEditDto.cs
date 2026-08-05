namespace Karim.Customer.HrApplication.Shared.DTOs.Payroll
{
    public class PayslipToEditDto
    {
        public required string Id { get; set; }
        public required decimal BasicSalary { get; set; }
        public decimal? TotalOvertime { get; set; }
        public decimal NetSalary { get; set; }
        public int Status { get; set; }
        public DateTime? PaidAt { get; set; }
        public int? PaymentWay { get; set; }
        public string? PaidNotes { get; set; }

    }
}
