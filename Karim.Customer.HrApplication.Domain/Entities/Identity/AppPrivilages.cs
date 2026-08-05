using Microsoft.AspNetCore.Identity;

namespace Karim.Customer.HrApplication.Domain.Entities.Identity
{
    public class AppPrivilages : IdentityRole
    {
        public decimal PrivNumber { get; set; }
    }
}
