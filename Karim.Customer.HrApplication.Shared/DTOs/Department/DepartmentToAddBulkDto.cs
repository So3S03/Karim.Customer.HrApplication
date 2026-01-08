namespace Karim.Customer.HrApplication.Shared.DTOs.Department
{
    public class DepartmentToAddBulkDto()
    {
        public string? DepartmentCode { get; set; }
        public string? DepartmentName { get; set; }
        public string? Description { get; set; }
        public DateTime ActualCreationDate { get; set; }
        public decimal TotalDepartmentBudget { get; set; }
        public decimal DepartmentBudgetForSalaries { get; set; }
        public decimal? DepartmentBudgetForTools { get; set; }
        public decimal? DepartmentBudgetForTrainees { get; set; }
        public decimal? DepartmentBudgetOther { get; set; }
        public int DepatrmentType { get; set; }
    }
}
