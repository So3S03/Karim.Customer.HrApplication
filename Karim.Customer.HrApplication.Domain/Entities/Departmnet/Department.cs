using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;

namespace Karim.Customer.HrApplication.Domain.Entities.Departmnet
{
    public class Department: BaseAuditableEntity<string>
    {
        public required string DepartmentName { get; set; }
        public DateTime ActualCreationDate { get; set; }
        public int TeamSize { get; set; }
        public decimal DepartmentBudget { get; set; }
        public int NumberOfProjects { get; set; }
        public int DepartmentEmployeesNumber { get; set; }
        public string? DepartmentPhotoUrl { get; set; }
    }
}
