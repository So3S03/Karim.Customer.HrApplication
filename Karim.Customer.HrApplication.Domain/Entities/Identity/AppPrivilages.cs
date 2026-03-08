using Microsoft.AspNetCore.Identity;

namespace Karim.Customer.HrApplication.Domain.Entities.Identity
{
    public class AppPrivilages : IdentityRole<string>
    {
        public decimal PrivNumber { get; set; }
    }
}
