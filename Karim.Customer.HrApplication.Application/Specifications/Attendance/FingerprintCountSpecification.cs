using Karim.Customer.HrApplication.Domain.Entities.Attendance;
using Karim.Customer.HrApplication.Shared.DTOs.Attendance;

namespace Karim.Customer.HrApplication.Application.Specifications.Attendance
{
    internal class FingerprintCountSpecification(FingerprintParameters parameters) : BaseSpecifications<Fingerprint, string>(
            AttendanceFuncCheckerGenerator.funcCompinor(
                    AttendanceFuncCheckerGenerator.getStatusFunc(parameters.Status)!,
                    AttendanceFuncCheckerGenerator.getByEmpId(parameters.EmpId)!,
                    AttendanceFuncCheckerGenerator.getDateFunc(parameters.From, parameters.To)!,
                    AttendanceFuncCheckerGenerator.searchByName(parameters.Name)!
                )
        ) 
    {
    }
}
