namespace Karim.Customer.HrApplication.Shared.DTOs.Payroll
{
    public class BonusToEditDto
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public required decimal Value { get; set; }
        public string? Description { get; set; }
    }
}
