namespace Karim.Customer.HrApplication.Shared.DTOs.Attendance
{
    public class EmployeeAttendanceStatusDto
    {
        public required IEnumerable<FingerprintSummaryDto> FingerprintSummary { get; set; }
        public required RequestsSummryDto RequestsSummary { get; set; }
        public int AbsentCount { get; set; }
        public int TotalAttendanceDays { get; set; }
        public decimal AttendancePercentage { get; set; }
    }
}
