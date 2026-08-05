using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.Entities.Employee;

namespace Karim.Customer.HrApplication.Domain.Entities.Attendance
{
    public class Requests : BaseAuditableEntity<string>
    {
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public string? Reason { get; set; }
        public string? Notes { get; set; }
        public required RequestStatus Status { get; set; }
        public required RequestType Type { get; set; }
        public string? ApprovedById { get; set; }
        public string? ApprovedByName { get; set; }
        public string? RejectedById { get; set; }
        public string? RejectedByName { get; set; }
        public decimal? Duration { get; set; }

        //relationships
        public required string EmpId { get; set; }
        public required Employee.Employee Employee { get; set; }

        public string? FingerprintId { get; set; }
        public Fingerprint? Fingerprint { get; set; }
    }
}
