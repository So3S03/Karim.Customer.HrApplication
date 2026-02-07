namespace Karim.Customer.HrApplication.Shared.DTOs.Department
{
    public class SingleDepartmentToReturnDto
    {
        public required string Id { get; set; }
        public required string DepartmentCode { get; set; }
        public required string DepartmentName { get; set; }
        public string? Description { get; set; }
        public bool isActive { get; set; }
        public bool isRemoved { get; set; }
        public DateTime ActualCreationDate { get; set; }
        public string? DepartmentPhotoUrl { get; set; }
        public decimal TotalDepartmentBudget { get; set; }
        public decimal DepartmentBudgetForSalaries { get; set; }
        public decimal? DepartmentBudgetForTools { get; set; }
        public decimal? DepartmentBudgetForTrainees { get; set; }
        public decimal? DepartmentBudgetOther { get; set; }
        public required string DepatrmentType { get; set; }
        public required string ManagerName { get; set; }
        public required string ManagerId { get; set; }
        public required string ManagerCode { get; set; }
        public required DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime ModifiedOn { get; set; }
        public required string ModifiedBy { get; set; }
    }
}
