using Karim.Customer.HrApplication.APIs.Controllers.Controllers.BaseController;
using Karim.Customer.HrApplication.Application.Abstraction.ManagerContract;
using Karim.Customer.HrApplication.Shared.DTOs.Attendance;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karim.Customer.HrApplication.APIs.Controllers.Controllers.Attendance
{
    public class AttendanceController(IServicesManager servicesManager) : ApiBaseController
    {
        [HttpGet("GetCurrentDayEmployeeFingerprint")]
        public async Task<ActionResult<SpecificFingerprintToReturnDto>> GetCurrentDayEmployeeFingerprnt(string? EmpId)
        {
            var result = await servicesManager.AttendanceService.GetFingerprintPerEmployeeForCurrentDay(EmpId);
            return Ok(result);
        }

        [HttpPost("InsertFingerprint")]
        public async Task<ActionResult<ActionStatusDto>> InsertFingerprint(FingerprintToBeInsertDto? fingerprint)
        {
            var result = await servicesManager.AttendanceService.InsertFingerprint(fingerprint);
            return Ok(result);
        }

        [HttpGet("FingerprintStatusLockUp")]
        public ActionResult<ICollection<EnumDto>> FingerprintStatusLockUp()
        {
            var result = servicesManager.AttendanceService.GetFingerPrintStatusLockup();
            return Ok(result);
        }

        [HttpGet("GetFingerprintById")]
        public async Task<ActionResult<FingerprintDetailsToReturnDto>> GetFingerprintById(string? Id)
        {
            var result = await servicesManager.AttendanceService.GetFingerprintById(Id);
            return Ok(result);
        }

        [HttpGet("GetCurrentMonthFingerprints")]
        public async Task<ActionResult<FingerprintToReturnDto>> GetAllFingerprints([FromQuery] FingerprintParameters? parameters)
        {
            var result = await servicesManager.AttendanceService.GetAllFingerprintLogs(parameters);
            return Ok(result);
        }

        [HttpPost("ManualAddFingerprint")]
        public async Task<ActionResult<ActionStatusDto>> AddEmployeeFingerprint(FingerprintToAddDto? fingerprint)
        {
            var result = await servicesManager.AttendanceService.InsertFingerprintManualyForEmployee(fingerprint);
            return Ok(result);
        }

        [HttpPut("EditEmployeeFingerprint")]
        public async Task<ActionResult<ActionStatusDto>> EditEmployeeFingerprint([FromBody] FingerprintToUpdateDto? fingerprint)
        {
            var result = await servicesManager.AttendanceService.EditEmployeeFingerprint(fingerprint);
            return Ok(result);
        }

        [HttpGet("DownloadBulkFingerprintTemplate")]
        public ActionResult<byte[]> DownloadBulkFingerprintTemplate()
        {
            var result = servicesManager.AttendanceService.GetUploadFingerprintBulk();
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BulkFingerprintTemplate.xlsx");
        }

        [HttpPost("UploadBulkFingerprints")]
        public async Task<ActionResult<ActionStatusDto>> UploadBulkFingerprints(IFormFile? file)
        {
            var result = await servicesManager.AttendanceService.UploadBulkFingerprintDto(file);
            return Ok(result);
        }

        [HttpGet("GetAttendanceSummaryPerEmployeeForCurrentMonth")]
        public async Task<ActionResult<EmployeeAttendanceStatusDto>> GetAttendanceSummaryPerEmployeeForCurrentMonth(string? EmpId)
        {
            var result = await servicesManager.AttendanceService.GetAttendanceSummaryPerEmployeeForCurrentMonth(EmpId);
            return Ok(result);
        }

        [HttpPost("CreateRequest")]
        public async Task<ActionResult<ActionStatusDto>> CreateRequest(RequestToAddDto? request)
        {
            var result = await servicesManager.AttendanceService.CreateRequest(request);
            return Ok(result);
        }
    }
}
