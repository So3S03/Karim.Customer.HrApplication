using Karim.Customer.HrApplication.APIs.Controllers.Controllers.BaseController;
using Karim.Customer.HrApplication.Application.Abstraction.ManagerContract;
using Karim.Customer.HrApplication.Shared.DTOs.Auth;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Karim.Customer.HrApplication.APIs.Controllers.Controllers
{
    public class AccountController(IServicesManager servicesManager) : ApiBaseController
    {
        [Authorize]
        [HttpPost("AddAccountForEmployee")]
        public async Task<ActionResult<ActionStatusDto>> SignUp(SignUpDto? User)
        {
            var result = await servicesManager.AuthService.EmployeeSignUp(User);
            return Ok(result);
        }

        [HttpPost("SignIn")]
        [EnableRateLimiting("SignInPolicy")]
        public async Task<ActionResult<SignInResultDto>> SignIn(SignInDto? User)
        {
            var response = Response;
            var result = await servicesManager.AuthService.SignIn(User, response);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetAllSystemPrivilages")]
        public async Task<ActionResult<ICollection<PrivilagesToReturnDto>>> GetAllPrivilages()
        {
            var result = await servicesManager.AuthService.GetAllPrivilages();
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetUserPrivilages")]
        public async Task<ActionResult<ICollection<PrivilagesToReturnDto>>> GetUserPrivilages(string? UserNameOrEmail)
        {
            var result = await servicesManager.AuthService.GetAllUserPrivilages(UserNameOrEmail);
            return Ok(result);
        }
        [Authorize]
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<SignInResultDto>> RefreshToken()
        {
            var result = await servicesManager.AuthService.RefreshingToken(Request, Response);
            return Ok(result);
        }
    }
}
