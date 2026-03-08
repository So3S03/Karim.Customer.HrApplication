using Karim.Customer.HrApplication.Shared.DTOs.Auth;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Microsoft.AspNetCore.Identity;

namespace Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Identity
{
    public interface IAuthServices
    {
        Task<ActionStatusDto> EmployeeSignUp(SignUpDto? user);
        Task<SignInResultDto> SignIn(SignInDto? user);
        Task<ICollection<PrivilagesToReturnDto>> GetAllPrivilages();
        Task<ICollection<PrivilagesToReturnDto>> GetAllUserPrivilages(string? userNameOrEmail);
    }
}
