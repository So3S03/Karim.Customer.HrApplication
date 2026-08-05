namespace Karim.Customer.HrApplication.Shared.DTOs.Employees.BulkUploadDtos
{
    public class BulkAddEmployeeDto()
    {
        public string? EmployeeCode { get; set; } //Must be EMP001
        public string? FullName { get; set; }
        public string? PersonalEmail { get; set; }
        public string? Position { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ExtraPhoneNumber { get; set; }
        public string? Address { get; set; }
        public int WorkType { get; set; }
        public int EmployeeType { get; set; }
        public int EmployeeRank { get; set; }
        public string? WorkLocation { get; set; }
        public DateTime? JoinDate { get; set; }
    }
}
