namespace Karim.Customer.HrApplication.Shared.DTOs.Payroll
{
    public class PayrollAllowanceToReturnDto
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public required decimal Value { get; set; }
        public string? Description { get; set; }
        public required string PayslipId { get; set; }
        public required DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public required bool isRemoved { get; set; }
        public DateTime? RemovedOn { get; set; }
        public string? RemovedBy { get; set; }
    }
}
