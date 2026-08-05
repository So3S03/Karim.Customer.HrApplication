namespace Karim.Customer.HrApplication.Shared.DTOs.Auth
{
    public class SignUpDto
    {
        public required string DisplayName { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PhoneNumber { get; set; }
        public List<decimal>? AsssignedPrivilages { get; set; }
        public required string EmpId { get; set; }
    }
}
