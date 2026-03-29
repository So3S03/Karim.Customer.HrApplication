using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.Entities.Employee;

namespace Karim.Customer.HrApplication.Domain.Entities.Attendance
{
    public class Fingerprint : BaseAuditableEntity<string>
    {
        public required TimeOnly CheckIn { get; set; }
        public TimeOnly? CheckOut { get; set; }
        public required DateOnly Date { get; set; }
        public decimal? DurationInHours { get; set; }
        public required decimal CheckInLong { get; set; }
        public required decimal CheckInLat { get; set; }
        public decimal? CheckOutLong { get; set; }
        public decimal? CheckOutLat { get; set; }
        public required FingerprintStatus Status { get; set; }

        //relationships
        public required string EmpId { get; set; }
        public required Employee.Employee Employee { get; set; }
        public string? RequestId { get; set; }
        public Requests? Request { get; set; }

    }
}
