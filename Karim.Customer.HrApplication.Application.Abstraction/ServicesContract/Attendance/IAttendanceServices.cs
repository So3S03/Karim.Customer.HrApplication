using Karim.Customer.HrApplication.Shared.DTOs.Attendance;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Attendance
{
    public interface IAttendanceServices
    {
        Task<ActionStatusDto> InsertFingerprint(FingerprintToBeInsertDto? fingerprint);
        Task<SpecificFingerprintToReturnDto> GetFingerprintPerEmployeeForCurrentDay(string? EmpId);
        ICollection<EnumDto> GetFingerPrintStatusLockup();
        Task<FingerprintDetailsToReturnDto> GetFingerprintById(string? Id);
    }
}
