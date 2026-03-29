namespace Karim.Customer.HrApplication.Shared.DTOs.Attendance
{
    public class FingerprintToReturnDto
    {
        public required string EmpId { get; set; }
        public required string FingerprintId { get; set; }
        public required string EmployeeName { get; set; }
        public DateOnly Date { get; set; }
        public string? CheckIn { get; set; }
        public string? CheckOut { get; set; }
        public decimal Duration { get; set; }
        public required string Status { get; set; }
    }
}
