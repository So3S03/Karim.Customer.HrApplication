namespace Karim.Customer.HrApplication.Shared.DTOs.Employees
{
    public class SingleEmployeeToAddDto
    {
        public required string EmployeeCode { get; set; } //Must be EMP001
        public required string FullName { get; set; }
        public string? PersonalEmail { get; set; }
        public string? WorkEmail { get; set; } //It Will Be The Email Employee Use For This App 
        public required string Position { get; set; }
        public required string PhoneNumber { get; set; }
        public string? ExtraPhoneNumber { get; set; }
        public string? Address { get; set; }
        public required int WorkType { get; set; }
        public required int EmployeeType { get; set; }
        public required int EmployeeRank { get; set; }
        public required string WorkLocation { get; set; }
        public DateTime? JoinDate { get; set; }
        //Department Relation
        public string? DepartmentId { get; set; }
    }
}
