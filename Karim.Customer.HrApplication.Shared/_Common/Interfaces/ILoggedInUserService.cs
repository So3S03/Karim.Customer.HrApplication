namespace Karim.Customer.HrApplication.Shared._Common.Interfaces
{
    public interface ILoggedInUserService
    {
        public string? UserId { get; }
        public string? UserName { get; }
    }
}
