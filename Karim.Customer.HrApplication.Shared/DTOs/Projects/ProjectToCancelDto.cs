namespace Karim.Customer.HrApplication.Shared.DTOs.Projects
{
    public class ProjectToCancelDto
    {
        public required string ProjectId { get; set; }
        public required string CancelationReason { get; set; }
    }
}
