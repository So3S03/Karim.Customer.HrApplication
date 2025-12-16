namespace Karim.Customer.HrApplication.Shared.DTOs.Department
{
    public class DepartmentToUpdateDto
    {
        public required string Id { get; set; }
        public required string DepartmentCode { get; set; }
        public required string DepartmentName { get; set; }
        public string? Description { get; set; }
        public DateTime ActualCreationDate { get; set; }
        public string? DepartmentPhotoUrl { get; set; }
        public required decimal TotalDepartmentBudget { get; set; }
        public required decimal DepartmentBudgetForSalaries { get; set; }
        public decimal? DepartmentBudgetForTools { get; set; }
        public decimal? DepartmentBudgetForTrainees { get; set; }
        public decimal? DepartmentBudgetOther { get; set; }
        public required int DepatrmentType { get; set; }
    }
}
