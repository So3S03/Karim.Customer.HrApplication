using Karim.Customer.HrApplication.Shared.DTOs.Attendance;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Microsoft.AspNetCore.Http;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Attendance
{
    public interface IAttendanceServices
    {
        Task<ActionStatusDto> InsertFingerprint(FingerprintToBeInsertDto? fingerprint);
        Task<SpecificFingerprintToReturnDto> GetFingerprintPerEmployeeForCurrentDay(string? EmpId);
        ICollection<EnumDto> GetFingerPrintStatusLockup();
        Task<FingerprintDetailsToReturnDto> GetFingerprintById(string? Id);
        Task<DataWithPagination<ICollection<FingerprintToReturnDto>>> GetAllFingerprintLogs(FingerprintParameters? fingerprintParameters);
        Task<ActionStatusDto> InsertFingerprintManualyForEmployee(FingerprintToAddDto? fingerprint);
        Task<ActionStatusDto> EditEmployeeFingerprint(FingerprintToUpdateDto? fingerprint);
        byte[] GetUploadFingerprintBulk();
        Task<ActionStatusDto> UploadBulkFingerprintDto(IFormFile? file);
        //Task<EmployeeAttendanceStatusDto> GetAttendanceSummaryPerEmployeeForCurrentMonth(string? EmpId);
        Task<ActionStatusDto> CreateRequest(RequestToAddDto? request);
        Task<ActionStatusDto> EditRequest(RequestToEditDto? request);
        Task<ActionStatusDto> DeleteRequest(string? ReqId);
        Task<ActionStatusDto> ApproveRejectRequest(string? ReqId, bool? isApproved, string? LoginEmpId);
        Task<RequestDetailsToReturnDto> GetRequestDetailsById(string? ReqId);
        Task<DataWithPagination<ICollection<RequestToReturnDto>>> GetAllRequests(RequestsParameters? parameters);
    }
}
