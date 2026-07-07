using Karim.Customer.HrApplication.Domain.Entities.Attendance;
using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.Entities.Identity;
using Karim.Customer.HrApplication.Domain.Entities.Tasks;
using department = Karim.Customer.HrApplication.Domain.Entities.Departmnet.Department;

namespace Karim.Customer.HrApplication.Domain.Entities.Employee
{
    public class Employee : BaseAuditableEntity<string>
    {
        public required string EmployeeCode { get; set; } //Must be EMP001
        public required string FullName { get; set; }
        public required string FullNameNormalized { get; set; }
        public string? PersonalEmail { get; set; }
        public string? WorkEmail { get; set; } //It Will Be The Email Employee Use For This App Determined On Auth Module
        public required string Position { get; set; }
        public string? PhotoUrl { get; set; }
        public required string PhoneNumber { get; set; }
        public string? ExtraPhoneNumber { get; set; }
        public string? Address { get; set; }
        public required WorkType WorkType { get; set; } //Determined On Contract Module
        public required EmployeeType EmployeeType { get; set; } //Determined On Contract Module
        public required string WorkLocation { get; set; }
        public bool IsHasContract { get; set; } //Determined On Contract Module
        public DateTime? ContractEndDate { get; set; } //Determined On Contract Module
        public decimal? Salary { get; set; } //Determined On Contract Module
        public DateTime JoinDate { get; set; }
        public required EmployeeRank Rank { get; set; }
        public EmployeeStatus? EmployeeStatus { get; set; } //Determined By Attendance Module

        //Department Relation
        public department? Department { get; set; }
        public string? DepartmentId { get; set; }

        //Manage Relation
        public department? ManagedDepartment { get; set; }

        //Account Relation
        public string? AccountId { get; set; }
        public AppUser? Account { get; set; }

        //Fingerprints
        public ICollection<Fingerprint>? FingerprintLog { get; set; }

        //Requests
        public ICollection<Requests>? Requests { get; set; }

        //Contract
        public Contracts.Contract? Contract { get; set; }

        //Tasks
        public ICollection<Tasks.Tasks>? Tasks { get; set; }

        //Payslips
        public ICollection<Payroll.Payslip>? Payslips { get; set; }
    }
}
