namespace Karim.Customer.HrApplication.Shared.DTOs.Projects
{
    public class ProjectDetailsToReturnDto
    {
        public required string Id { get; set; }
        public required string ProjectCode { get; set; }
        public required string ProjectName { get; set; }
        public string? Description { get; set; }
        public required string ProjectType { get; set; }
        public required string ProjectStatus { get; set; }
        public required decimal HoursAmount { get; set; }
        public DateTime? ActivatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? CanceledAt { get; set; }
        public string? CancelationReason { get; set; }
        public required decimal CompletionPercentage { get; set; }
        public required decimal ProjectCoast { get; set; }
        public required string CoastCurrency { get; set; }
        public string? DepartmentId { get; set; }
        public string? Department { get; set; }
        public required DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public required bool isRemoved { get; set; } = false;
        public DateTime? RemovedOn { get; set; }
        public string? RemovedBy { get; set; }
    }
}
