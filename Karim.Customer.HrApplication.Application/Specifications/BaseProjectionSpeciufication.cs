using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.Specifications;
using System.Linq.Expressions;

namespace Karim.Customer.HrApplication.Application.Specifications
{
    internal class BaseProjectionSpeciufication<TEntity, TKey, TGroupKey, TResult> : IProjectionSpecification<TEntity, TKey, TGroupKey, TResult>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
        where TResult : class
    {
        public Expression<Func<TEntity, bool>>? Criteria {  get; private set; }

        public Expression<Func<TEntity, TGroupKey>>? GroupBy { get; private set; }

        public Expression<Func<IGrouping<TGroupKey, TEntity>, TResult>>? SelectProjection { get; private set; }

        protected BaseProjectionSpeciufication(Expression<Func<TEntity, bool>>? expression)
        {
            Criteria = expression;
        }

        protected void setGroupBy(Expression<Func<TEntity, TGroupKey>>? expression)
        {
            GroupBy = expression;
        }

        protected void setSelector(Expression<Func<IGrouping<TGroupKey, TEntity>, TResult>> expression)
        {
            SelectProjection = expression;
        }
    }
}
