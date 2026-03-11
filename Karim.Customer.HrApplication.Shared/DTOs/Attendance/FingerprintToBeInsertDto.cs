namespace Karim.Customer.HrApplication.Shared.DTOs.Attendance
{
    public class FingerprintToBeInsertDto
    {
        public TimeOnly? CheckIn { get; set; }
        public TimeOnly? CheckOut { get; set; }
        public required DateOnly Date { get; set; }
        public required decimal Long { get; set; }
        public required decimal Lat { get; set; }
        public required int Status { get; set; }
        public required string EmpId { get; set; }
    }
}
