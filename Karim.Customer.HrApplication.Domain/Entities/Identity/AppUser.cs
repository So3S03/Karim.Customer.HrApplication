using Karim.Customer.HrApplication.Domain.Entities.Employee;
using Microsoft.AspNetCore.Identity;

namespace Karim.Customer.HrApplication.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public AppUser()
        {
            Id = new Guid().ToString();
        }
        public Employee.Employee? Employee { get; set; }
        public string? EmpId { get; set; }
        public required string DisplayName { get; set; }
        public bool isSuspended { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string? LastLoginIp { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool isRemoved { get; set; }
        public string? RemovedBy { get; set; }
        public DateTime? RemovedOn { get; set; }
    }
}
