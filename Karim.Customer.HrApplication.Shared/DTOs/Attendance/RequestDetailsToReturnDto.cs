namespace Karim.Customer.HrApplication.Shared.DTOs.Attendance
{
    public class RequestDetailsToReturnDto
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
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public bool isRemoved { get; set; }
        public DateTime? RemovedOn { get; set; }
        public string? RemovedBy { get; set; }
    }
}
