namespace Karim.Customer.HrApplication.Shared.DTOs.Employees
{
    public class EmployeeToTermenteDto
    {
        public required string Id { get; set; }
        public required string EmployeeCode { get; set; } //Must be EMP001
        public required string FullName { get; set; }
        public string? WorkEmail { get; set; }
        public required string Position { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Rank { get; set; }
        public string? PhotoUrl { get; set; }
        public required string WorkType { get; set; }
        public required string EmployeeType { get; set; }
        public required string WorkLocation { get; set; }
        public bool IsHasContract { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public DateTime JoinDate { get; set; }
        public string EmployeeStatus { get; set; }
        public string? Department { get; set; }
        public string? DepartmentCode { get; set; }
    }
}
