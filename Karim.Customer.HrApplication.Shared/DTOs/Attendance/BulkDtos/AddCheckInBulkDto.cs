namespace Karim.Customer.HrApplication.Shared.DTOs.Attendance.BulkDtos
{
    public class AddCheckInBulkDto()
    {
        public TimeOnly? CheckIn { get; set; }
        public TimeOnly? CheckOut { get; set; }
        public string? EmpCode { get; set; }
    }
}
