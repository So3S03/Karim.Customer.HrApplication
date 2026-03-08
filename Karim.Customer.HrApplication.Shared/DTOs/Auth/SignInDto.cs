namespace Karim.Customer.HrApplication.Shared.DTOs.Auth
{
    public class SignInDto
    {
        public required string UserNameOrEmail { get; set; }
        public required string Password { get; set; }
    }
}
