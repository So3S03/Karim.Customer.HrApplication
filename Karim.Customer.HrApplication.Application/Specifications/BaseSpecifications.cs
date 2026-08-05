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
        public Expression<Func<TEntity, object>>? OrderBy { get; private set; } = default!;
        public Expression<Func<TEntity, object>>? OrderByDesc { get; private set; } = default!;
        public int Skip { get; private set; }
        public int Take { get; private set; }
        public bool IsPagination { get; private set; }


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

        protected void SetOrderByAsc(Expression<Func<TEntity, object>> orderBy)
        {
            OrderBy = orderBy;
        }

        protected void SetOrderByDesc(Expression<Func<TEntity, object>> orderByDesc)
        {
            OrderByDesc = orderByDesc;
        }

        protected void Pagination(int page, int pageSize)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0 || pageSize > 9) pageSize = 9;
            Skip = (page - 1) * pageSize;
            Take = pageSize;
            IsPagination = true;
        }
    }
}
