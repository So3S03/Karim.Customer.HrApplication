namespace Karim.Customer.HrApplication.Shared.DTOs.Payroll
{
    public class PenaltyToAddDto
    {
        public required string PayslipId { get; set; }
        public required string Title { get; set; }
        public required decimal Value { get; set; }
        public string? Description { get; set; }
    }
}
