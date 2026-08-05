using Karim.Customer.HrApplication.Domain.Entities.Attendance;
using Karim.Customer.HrApplication.Shared.DTOs.Attendance;
using System.Linq.Expressions;

namespace Karim.Customer.HrApplication.Application.Specifications.Attendance
{
    internal class FinerprintListSpecifications : BaseSpecifications<Fingerprint, string>
    {
        public FinerprintListSpecifications(FingerprintParameters fingerprintParameters): base(
                AttendanceFuncCheckerGenerator.funcCompinor(
                        AttendanceFuncCheckerGenerator.getDateFunc(fingerprintParameters.From, fingerprintParameters.To)!,
                        AttendanceFuncCheckerGenerator.getStatusFunc(fingerprintParameters.Status)!,
                        AttendanceFuncCheckerGenerator.searchByName(fingerprintParameters.Name)!,
                        AttendanceFuncCheckerGenerator.getByEmpId(fingerprintParameters.EmpId)!
                    )
            )
        {
            AddInclude(FB => FB.Employee);
            SetOrderByAsc(FB => FB.Date);
            Pagination(fingerprintParameters.PageNum, fingerprintParameters.PageSize!);
        }
    }
}
