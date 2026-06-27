namespace Karim.Customer.HrApplication.Shared.DTOs.Attendance
{
    public class RequestToAddDto
    {
        public required DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Reason { get; set; }
        public string? Notes { get; set; }
        public decimal? OverTimeHours { get; set; }
        public required int Type { get; set; }
        //public int? Duration { get; set; }

        //relationships
        public required string EmpId { get; set; }
    }
}
