using Karim.Customer.HrApplication.APIs.Controllers.Controllers.BaseController;
using Karim.Customer.HrApplication.Application.Abstraction.ManagerContract;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.DTOs.Department;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Karim.Customer.HrApplication.APIs.Controllers.Controllers.Department
{
    public class DepartmentController(IServicesManager servicesManager) : ApiBaseController
    {
        [HttpGet("GetAllDepartment")]
        public async Task<ActionResult<DataWithPagination<ICollection<DepartmentToReturnDto>>>> GetAllDepartments([FromQuery] DepartmentQueryParameters parameters)
        {
            var result = await servicesManager.DepartmentService.GetDepartmentsAsync(parameters);
            return Ok(result);
        }

        [HttpGet("FillDepartmentStatusLockUp")]
        public ActionResult<ICollection<EnumDto>> FillDepartmentStatusLockUp()
        {
            var result = servicesManager.DepartmentService.FillDepartmentsStatus();
            return Ok(result);
        }

        [HttpGet("FillDepartmentTypesLockUp")]
        public ActionResult<ICollection<EnumDto>> FillDepartmentTypesLockUp()
        {
            var result = servicesManager.DepartmentService.FillDepartmentTypes();
            return Ok(result);
        }

        [HttpGet("DepartmentSorrtingLockUp")]
        public ActionResult<ICollection<EnumDto>> DepartmentSorrtingLockUp()
        {
            var result = servicesManager.DepartmentService.DepartmentSortingLockUp();
            return Ok(result);
        }

        [HttpGet("GetDepartmentById")]
        public async Task<ActionResult<DepartmentToReturnDto>> GetDepartmentById(string? Id)
        {
            var result = await servicesManager.DepartmentService.GetDepartmentByIdAsync(Id);
            return Ok(result);
        }

        [HttpPut("DepartmentActiveToggle")]
        public async Task<ActionResult<ActionStatusDto>> ActivationToggleForDepartment(string? id, bool? status)
        {
            var Result = await servicesManager.DepartmentService.DepartmentActiveToggle(id, status);
            return Ok(Result);
        }

        [HttpPut("SoftRemoveDepartment")]
        public async Task<ActionResult<ActionStatusDto>> SoftDeleteDepartment(string? id)
        {
            var Result = await servicesManager.DepartmentService.SoftRemoveDepartment(id);
            return Ok(Result);
        }

        [HttpPut("RestoreDepartment")]
        public async Task<ActionResult<ActionStatusDto>> ActivationToggleForDepartment(string? id)
        {
            var Result = await servicesManager.DepartmentService.RestoreRemovedDepartment(id);
            return Ok(Result);
        }

        [HttpPost("AddDepartment")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ActionStatusDto>> AddDepartment([FromForm] DepartmentToAddDto? entity, IFormFile? file)
        {
            var result = await servicesManager.DepartmentService.AddDepartmentAsync(entity, file);
            return Ok(result);
        }

        [HttpPut("UpdateDepartment")]
        public async Task<ActionResult<ActionStatusDto>> UpdateDepartment([FromForm] DepartmentToUpdateDto? entity, IFormFile? file)
        {
            var result = await servicesManager.DepartmentService.UpdateDepartment(entity, file);
            return Ok(result);
        }

        [HttpDelete("DeleteDepartment")]
        public async Task<ActionResult<ActionStatusDto>> DeleteDepartment(string? id)
        {
            var result = await servicesManager.DepartmentService.DeleteDepartment(id);
            return Ok(result);
        }

        [HttpDelete("DeleteDepartmentPhoto")]
        public async Task<ActionResult<ActionStatusDto>> DeleteDepartmentPhoto(string? id)
        {
            var result = await servicesManager.DepartmentService.DeletePhoto(id);
            return Ok(result);
        }

        [HttpGet("GetDepartmentExcelSheetForAddBulkTemplate")]
        public ActionResult GetDepartmentExcelSheetTemplate()
        {
            var result = servicesManager.DepartmentService.GenerateDepartmentTemplateExcelSheetForAddRange();
            var file = File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DepartmentToAddExcelTemplate.xlsx");
            return file;
        }

        [HttpGet("GetDepartmentsListExcelSheet")]
        public async Task<ActionResult> GetDepartmentsListExcelSheet()
        {
            var result = await servicesManager.DepartmentService.GenerateDepartmentsListExcelSheet();
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DepartmentsListExcelSheet.xlsx");
        }

        [HttpPost("UploadBulkDepartmentsForAdd")]
        public async Task<ActionResult<ActionStatusDto>> UploadBulkDepartmentsForAdd(IFormFile? file)
        {
            var result = await servicesManager.DepartmentService.UploadBulkDepartmentsForAdd(file);
            return Ok(result);
        }

        [HttpGet("FillDepartmentColumnsLockUp")]
        public ActionResult<ICollection<EnumDto>> FillDepartmentColumnsLockUp()
        {
            var result = servicesManager.DepartmentService.GetDepartmentColumns();
            return Ok(result);
        }

        [HttpGet("GenerateExcelSheetForUpdate")]
        public async Task<ActionResult<ActionStatusDto>> GenerateEcelSheetForUpdate(int? columnToBeUpdated)
        {
            var result = await servicesManager.DepartmentService.GenerateDepartmentListExcelSheetForUpdateRange(columnToBeUpdated);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DepartmentToUpdateExcelTemplate.xlsx");
        }

        [HttpPut("UploadBulkDepartmentsForUpdate")]
        public async Task<ActionResult<ActionStatusDto>> UploadBulkDepartmentsForUpdate(IFormFile? file, int? columnToBeUpdated)
        {
            var result = await servicesManager.DepartmentService.UploadBulkDepartmentsForUpdate(file, columnToBeUpdated);
            return Ok(result);
        }

        [HttpGet("GenerateMaxDepartmentCode")]
        public async Task<ActionResult<MaxCodeResult>> GenerateMaxDepartmentCode()
        {
            var result = await servicesManager.DepartmentService.GenerateMaxDepartmentCode();
            return Ok(result);
        }
    }
}
