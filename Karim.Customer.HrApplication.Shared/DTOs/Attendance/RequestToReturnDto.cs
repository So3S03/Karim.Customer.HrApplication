namespace Karim.Customer.HrApplication.Shared.DTOs.Attendance
{
    public class RequestToReturnDto
    {
        public string Id { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public string? Reason { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string? ApprovedById { get; set; }
        public string? ApprovedByName { get; set; }
        public string? RejectedById { get; set; }
        public string? RejectedByName { get; set; }
        public string EmpId { get; set; }
        public string EmployeeName { get; set; }
        public string? FingerprintId { get; set; }
    }
}
