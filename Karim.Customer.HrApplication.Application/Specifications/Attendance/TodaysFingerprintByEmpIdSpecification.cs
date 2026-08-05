using Karim.Customer.HrApplication.Domain.Entities.Attendance;

namespace Karim.Customer.HrApplication.Application.Specifications.Attendance
{
    internal class TodaysFingerprintByEmpIdSpecification: BaseSpecifications<Fingerprint, string>
    {
        public TodaysFingerprintByEmpIdSpecification(string EmpId, DateOnly date): base(F => F.EmpId == EmpId && F.Date == date)
        {
            AddInclude(F => F.Employee);
        }
    };
}
