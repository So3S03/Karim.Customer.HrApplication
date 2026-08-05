namespace Karim.Customer.HrApplication.Shared.DTOs.Payroll
{
    public class AllowanceToAddDto
    {
        public required string Title { get; set; }
        public required decimal Value { get; set; }
        public string? Description { get; set; }
        public required string PayslipId { get; set; }
    }
}
