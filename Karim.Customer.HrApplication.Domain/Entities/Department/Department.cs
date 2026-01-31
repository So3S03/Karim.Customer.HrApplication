using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.Entities.Department;
using Karim.Customer.HrApplication.Domain.Entities.Employee;

namespace Karim.Customer.HrApplication.Domain.Entities.Departmnet
{
    public class Department : BaseAuditableEntity<string>
    {
        public required string DepartmentCode { get; set; }
        public required string DepartmentName { get; set; }
        public required string NormalizedName { get; set; } //For Searching
        public string? Description { get; set; }
        public bool isActive { get; set; }
        public bool isRemoved { get; set; } //For Soft Delete
        public DateTime ActualCreationDate { get; set; }
        public string? DepartmentPhotoUrl { get; set; }
        public decimal TotalDepartmentBudget { get; set; }
        public decimal DepartmentBudgetForSalaries { get; set; }
        public decimal? DepartmentBudgetForTools { get; set; }
        public decimal? DepartmentBudgetForTrainees { get; set; }
        public decimal? DepartmentBudgetOther { get; set; }
        public DepartmentType DepatrmentType { get; set; }

        //Many Emps Relation
        public virtual ICollection<Employee.Employee>? Employees { get; set; }

        //One Manager Relation
        public virtual Employee.Employee? Manager { get; set; }
        public string? ManagerId { get; set; }
    }
}
