namespace Karim.Customer.HrApplication.Shared.DTOs.Attendance
{
    public class EmployeeAttendanceStatusDto
    {
        //Came From Request Entity Query
        public int VacationDays { get; set; }
        public int LeaveDays { get; set; }
        public int PermissionDays { get; set; }
        public int OverTimeDays { get; set; }
        //Came From Fingerprint Entity Query
        public int LateForWorkDays { get; set; }
        public int DelayInDurationDays { get; set; }
        public int TotalAttendanceDays { get; set; }
        public decimal AttendancePercentage { get; set; }
        public int AbsensCount { get; set; }
    }
}
