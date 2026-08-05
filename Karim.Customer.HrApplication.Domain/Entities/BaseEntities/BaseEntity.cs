namespace Karim.Customer.HrApplication.Domain.Entities.BaseEntities
{
    public abstract class BaseEntity<TKey>
        where TKey : IEquatable<TKey> 
    {
        public TKey Id { get; set; }
    }
}
