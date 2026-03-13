namespace Karim.Customer.HrApplication.Shared.DTOs.Attendance
{
    public class FingerprintDetailsToReturnDto
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
        public required DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public required string CreatedBy { get; set; } = "1";
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public required bool isRemoved { get; set; } = false;
        public DateTime? RemovedOn { get; set; }
        public string? RemovedBy { get; set; }
    }
}
