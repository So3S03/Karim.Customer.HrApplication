using Karim.Customer.HrApplication.Domain.Entities.Attendance;
using Karim.Customer.HrApplication.Shared.DTOs.Attendance;

namespace Karim.Customer.HrApplication.Application.Specifications.Attendance
{
    internal class FingerprintSummarySpecification : BaseProjectionSpeciufication<Fingerprint, string, FingerprintStatus, FingerprintSummaryDto>
    {
        public FingerprintSummarySpecification(string? EmpId, DateOnly? From, DateOnly? To): base(
                AttendanceFuncCheckerGenerator.funcCompinor(
                        AttendanceFuncCheckerGenerator.getByEmpId(EmpId)!,
                        AttendanceFuncCheckerGenerator.getDateFunc(From, To)!
                    )
            )
        {
            setGroupBy(FP => FP.Status);
            setSelector(ele => new FingerprintSummaryDto()
            {
                DelayInDurationCount = ele.Count(c => c.Status == FingerprintStatus.Delay),
                LateForWorkCount = ele.Count(c => c.Status == FingerprintStatus.Late)
            });
        }
    }
}
