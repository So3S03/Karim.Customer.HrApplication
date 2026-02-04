namespace Karim.Customer.HrApplication.Shared.DTOs.Employees
{
    public class EmployeeToReturnDto
    {
        public required string EmployeeCode { get; set; } //Must be EMP001
        public required string FullName { get; set; }
        public string? PersonalEmail { get; set; }
        public string? WorkEmail { get; set; }
        public required string Position { get; set; }
        public required string PhoneNumber { get; set; }
        public string? PhotoUrl { get; set; }
        public string? ExtraPhoneNumber { get; set; }
        public string? Address { get; set; }
        public required string WorkType { get; set; }
        public required string EmployeeType { get; set; }
        public required string WorkLocation { get; set; }
        public bool IsHasContract { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public int? ContractStatusId { get; set; }
        public string? ContractStatusName { get; set; }
        public decimal? Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public string EmployeeStatus { get; set; }
        public string? Department { get; set; }
        public string? DepartmentId { get; set; }
        public string? DepartmentCode { get; set; }
    }
}
