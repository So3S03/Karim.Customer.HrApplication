using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using System.Linq.Expressions;

namespace Karim.Customer.HrApplication.Domain.Specifications
{
    public interface IProjectionSpecification<TEntity, TKey, TGroupKey, TResult>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
        where TResult : class
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; }
        public Expression<Func<TEntity, TGroupKey>>? GroupBy { get; }
        public Expression<Func<IGrouping<TGroupKey, TEntity>, TResult>>? SelectProjection { get; }
    }
}
