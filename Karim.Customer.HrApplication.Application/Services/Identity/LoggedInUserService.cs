using Karim.Customer.HrApplication.Shared._Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Karim.Customer.HrApplication.Application.Services.Identity
{
    public class LoggedInUserService(IHttpContextAccessor _httpContextAccessor) : ILoggedInUserService
    {
        public string? UserId =>
        _httpContextAccessor.HttpContext?.User?.FindFirstValue("AccountId") ?? "System";

        public string? UserName =>
            _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name) ?? "System";
    }
}
