namespace Karim.Customer.HrApplication.Shared.DTOs.Payroll
{
    public class PayrollToPayDto
    {
        public required string PayslipId { get; set; }
        public required int PaymentWay { get; set; }
        public string? PaidNote { get; set; }
    }
}
