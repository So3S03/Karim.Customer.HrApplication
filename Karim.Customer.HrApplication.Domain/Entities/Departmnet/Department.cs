using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;

namespace Karim.Customer.HrApplication.Domain.Entities.Departmnet
{
    public class Department: BaseAuditableEntity<string>
    {
        public required string DepartmentCode { get; set; }
        public required string DepartmentName { get; set; }
        public required string NormalizedName { get; set; } //For Searching
        public string? Description { get; set; }
        public bool isActive { get; set; }
        public DateTime ActualCreationDate { get; set; }
        public string? DepartmentPhotoUrl { get; set; }
        public decimal TotalDepartmentBudget { get; set; }
        public decimal DepartmentBudgetForSalaries { get; set; }
        public decimal DepartmentBudgetForTools { get; set; }
        public decimal DepartmentBudgetForTrainees { get; set; }
        public decimal? DepartmentBudgetOther { get; set; }
        //public int NumberOfProjects { get; set; } //it will came from project module
        //public int DepartmentEmployeesNumber { get; set; } //it will came from employee module
    }
}
