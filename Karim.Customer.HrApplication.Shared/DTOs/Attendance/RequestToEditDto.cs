namespace Karim.Customer.HrApplication.Shared.DTOs.Attendance
{
    public class RequestToEditDto
    {
        public required string Id { get; set; }
        public required DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Reason { get; set; }
        public string? Notes { get; set; }
        public decimal? Duration { get; set; }
        public required int Type { get; set; }
        public required string EmpId { get; set; }
    }
}
