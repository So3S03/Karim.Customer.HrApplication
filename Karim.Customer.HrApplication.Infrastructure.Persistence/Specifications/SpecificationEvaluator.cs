using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> entryPoint, ISpecifications<TEntity, TKey> specifications)
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            var Query = entryPoint;
            if (specifications is not null)
            {
                if (specifications.Criteria is not null)
                {
                    Query = Query.Where(specifications.Criteria);
                }
                if (specifications.IncludeList is not null && specifications.IncludeList.Any())
                {
                    Query = specifications.IncludeList.Aggregate(Query, (currentQuery, include) => currentQuery.Include(include));
                }
                if (specifications.OrderBy is not null)
                {
                    Query = Query.OrderBy(specifications.OrderBy);
                }
                else if (specifications.OrderByDesc is not null)
                {
                    Query = Query.OrderByDescending(specifications.OrderByDesc);
                }
            }
            return Query;
        }
    }
}
