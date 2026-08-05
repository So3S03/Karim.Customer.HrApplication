namespace Karim.Customer.HrApplication.Shared.DTOs.Dashboard
{
    public class AllEmployeesAttendanceRatePerMonthDto
    {
        public required string Month { get; set; }
        public decimal AttendanceRate { get; set; }
    }
}
