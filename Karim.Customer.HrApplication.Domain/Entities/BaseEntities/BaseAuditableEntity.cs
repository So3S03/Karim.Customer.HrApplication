namespace Karim.Customer.HrApplication.Domain.Entities.BaseEntities
{
    public abstract class BaseAuditableEntity<TKey> : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public required DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public required bool isRemoved { get; set; } = false;
        public DateTime? RemovedOn { get; set; }
        public string? RemovedBy { get; set; }
    }
}
