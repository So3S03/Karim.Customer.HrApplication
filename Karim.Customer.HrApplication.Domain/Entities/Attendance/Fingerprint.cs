using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.Entities.Employee;

namespace Karim.Customer.HrApplication.Domain.Entities.Attendance
{
    public class Fingerprint : BaseAuditableEntity<string>
    {
        public required TimeOnly CheckIn { get; set; }
        public TimeOnly? CheckOut { get; set; }
        public required DateOnly Date { get; set; }
        public int? DurationInHours { get; set; }
        public required decimal Long { get; set; }
        public required decimal Lat { get; set; }
        public required FingerprintStatus Status { get; set; }
        public required string EmpId { get; set; }
        public required Employee.Employee Employee { get; set; }

    }
}
