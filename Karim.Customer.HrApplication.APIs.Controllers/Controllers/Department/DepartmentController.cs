using Karim.Customer.HrApplication.APIs.Controllers.Controllers.BaseController;
using Karim.Customer.HrApplication.Shared.DTOs.Department;
using Microsoft.AspNetCore.Mvc;

namespace Karim.Customer.HrApplication.APIs.Controllers.Controllers.Department
{
    public class DepartmentController : ApiBaseController
    {
        [HttpGet("GetAllDepartment")]
        public async Task<ActionResult<ICollection<DepartmentToReturnDto>>> GetAllDepartments([FromQuery]int type = 0) //0 = all -- 1 = removed -- 2 = not removed -- isActive = 3 -- isNotActive = 4
        {
            return Ok("Done");
        }
        [HttpGet("FillDepartmentLockUp")]
        public async Task<ActionResult> FillDepartmentLockup()
        {
            return Ok();
        }
    }
}
