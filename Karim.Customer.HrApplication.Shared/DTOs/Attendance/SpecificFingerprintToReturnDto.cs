namespace Karim.Customer.HrApplication.Shared.DTOs.Attendance
{
    public class SpecificFingerprintToReturnDto
    {
        public string? Id { get; set; }
        public TimeOnly? CheckIn { get; set; }
        public TimeOnly? CheckOut { get; set; }
        public DateOnly Date { get; set; }
        public decimal? Long { get; set; }
        public decimal? Lat { get; set; }
        public required string Status { get; set; }
        public required string EmpId { get; set; }
        public required string EmployeeName { get; set; }
    }
}
