namespace Karim.Customer.HrApplication.Domain.Entities.BaseEntities
{
    public abstract class BaseAuditableEntity<TKey> : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public required DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime ModifiedOn { get; set; }
        public required string ModifiedBy { get; set; }
    }
}
