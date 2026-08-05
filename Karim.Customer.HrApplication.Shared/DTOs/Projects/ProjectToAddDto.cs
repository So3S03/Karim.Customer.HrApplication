namespace Karim.Customer.HrApplication.Shared.DTOs.Projects
{
    public class ProjectToAddDto
    {
        public required string ProjectCode { get; set; }
        public required string ProjectName { get; set; }
        public string? Description { get; set; }
        public required int ProjectType { get; set; }
        public required decimal HoursAmount { get; set; }
        public required decimal ProjectCoast { get; set; }
        public required int CoastCurrency { get; set; }
    }
}
