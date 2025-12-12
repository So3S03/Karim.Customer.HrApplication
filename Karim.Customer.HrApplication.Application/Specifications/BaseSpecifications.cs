using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.Specifications;
using System.Linq.Expressions;

namespace Karim.Customer.HrApplication.Application.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeList { get; } = [];

        public Expression<Func<TEntity, bool>>? Criteria { get; } = default!;

        protected BaseSpecifications()
        {
            
        }

        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }

        protected void AddInclude(Expression<Func<TEntity, object>> include)
        {
            IncludeList.Add(include);
        }
    }
}
