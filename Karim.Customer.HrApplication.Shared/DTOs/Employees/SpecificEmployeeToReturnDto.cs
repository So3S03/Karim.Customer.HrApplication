using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace Karim.Customer.HrApplication.Shared.DTOs.Employees
{
    public class SpecificEmployeeToReturnDto
    {
        public required string Id { get; set; }
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
        public required string WorkType { get; set; }
        public required string EmployeeType { get; set; }
        public required string WorkLocation { get; set; }
        public decimal? Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public required string EmployeeStatus { get; set; }
        public string? Department { get; set; }
        public string? DepartmentId { get; set; }
        public string? DepartmentCode { get; set; }
        public bool IsHasContract { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public required DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime ModifiedOn { get; set; }
        public required string ModifiedBy { get; set; }
    }
}
