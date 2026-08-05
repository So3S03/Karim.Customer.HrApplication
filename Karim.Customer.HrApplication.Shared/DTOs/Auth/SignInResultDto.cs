using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;

namespace Karim.Customer.HrApplication.Shared.DTOs.Auth
{
    public class SignInResultDto
    {
        public required ActionStatusDto Status { get; set; }
        public required string Token { get; set; }
    }
}
