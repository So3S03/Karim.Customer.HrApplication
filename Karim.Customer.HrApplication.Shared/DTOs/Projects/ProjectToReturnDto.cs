namespace Karim.Customer.HrApplication.Shared.DTOs.Projects
{
    public class ProjectToReturnDto
    {
        public required string ProjectCode { get; set; }
        public required string ProjectName { get; set; }
        public string? Description { get; set; }
        public required int ProjectType { get; set; }
        public required int ProjectStatus { get; set; }
        public DateTime? ActivatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? CanceledAt { get; set; }
        public string? CancelationReason { get; set; }
        public required decimal CompletionPercentage { get; set; }
        public required decimal ProjectCoast { get; set; }
        public required int CoastCurrency { get; set; }
        public string? DepartmentId { get; set; }
    }
}
