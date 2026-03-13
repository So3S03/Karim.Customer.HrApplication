namespace Karim.Customer.HrApplication.Shared.DTOs.Attendance
{
    public class FingerprintToBeInsertDto
    {
        public required decimal Long { get; set; }
        public required decimal Lat { get; set; }
        public required string EmpId { get; set; }
    }
}
