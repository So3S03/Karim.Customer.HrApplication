namespace Karim.Customer.HrApplication.Shared.DTOs.Projects
{
    public class ProjectToUpdateDto
    {
        public required string Id { get; set; }
        public required string ProjectCode { get; set; }
        public required string ProjectName { get; set; }
        public string? Description { get; set; }
        public required int ProjectType { get; set; }
        public required decimal ProjectCoast { get; set; }
        public required int CoastCurrency { get; set; }
        public required decimal HoursAmount { get; set; }
    }
}
