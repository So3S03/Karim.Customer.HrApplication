using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;

namespace Karim.Customer.HrApplication.Domain.Entities.Identity
{
    public class RefreshToken : BaseEntity<string>
    {
        public string TokenHash { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
