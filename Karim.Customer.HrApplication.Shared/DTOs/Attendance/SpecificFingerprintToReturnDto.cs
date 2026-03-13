namespace Karim.Customer.HrApplication.Shared.DTOs.Attendance
{
    public class SpecificFingerprintToReturnDto
    {
        public string? Id { get; set; }
        public string? CheckIn { get; set; }
        public string? CheckOut { get; set; }
        public DateOnly Date { get; set; }
        public decimal? DurationInHours { get; set; }
        public decimal? CheckInLong { get; set; }
        public decimal? CheckInLat { get; set; }
        public decimal? CheckOutLong { get; set; }
        public decimal? CheckOutLat { get; set; }
        public required string Status { get; set; }
        public required string EmpId { get; set; }
        public required string EmployeeName { get; set; }
    }
}
