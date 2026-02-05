using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using department = Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;

namespace Karim.Customer.HrApplication.Domain.Entities.Employee
{
    public class Employee : BaseAuditableEntity<string>
    {
        public required string EmployeeCode { get; set; } //Must be EMP001
        public required string FullName { get; set; }
        public required string FullNameNormalized { get; set; }
        public string? PersonalEmail { get; set; }
        public string? WorkEmail { get; set; } //It Will Be The Email Employee Use For This App 
        public required string Position { get; set; }
        public string? PhotoUrl { get; set; }
        public required string PhoneNumber { get; set; }
        public string? ExtraPhoneNumber { get; set; }
        public string? Address { get; set; }
        public required WorkType WorkType { get; set; }
        public required EmployeeType EmployeeType { get; set; }
        public required string WorkLocation { get; set; }
        public bool IsHasContract { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public decimal? Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; }

        //Department Relation
        public department? Department { get; set; }
        public string? DepartmentId { get; set; }

        //Manage Relation
        public department? ManagedDepartment { get; set; }
    }
}
