using Karim.Customer.HrApplication.APIs.Controllers.Controllers.BaseController;
using Karim.Customer.HrApplication.Application.Abstraction.ManagerContract;
using Karim.Customer.HrApplication.Shared.DTOs.Attendance;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
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
    }
}
