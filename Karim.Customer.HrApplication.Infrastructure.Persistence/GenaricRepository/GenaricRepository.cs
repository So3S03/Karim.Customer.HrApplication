using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.GenaricRepository;
using Karim.Customer.HrApplication.Domain.Specifications;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.GenaricRepository
{
    public class GenaricRepository<TEntity, TKey>(DbContext dbContext) : IGenaricRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications) => await Evaluator(specifications).ToListAsync();

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications) => await Evaluator(specifications).FirstOrDefaultAsync();

        public async Task<TEntity?> GetByIdAsyncWithNoTracking(ISpecifications<TEntity, TKey> specifications) => await Evaluator(specifications).AsNoTracking().FirstOrDefaultAsync();

        public async Task AddAsync(TEntity entity) => await dbContext.Set<TEntity>().AddAsync(entity);

        public Task<int> GetDataCountAsync(ISpecifications<TEntity, TKey> specifications) => Evaluator(specifications).CountAsync();

        public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await dbContext.Set<TEntity>().AddRangeAsync(entities); //it will be for upload bulk methods

        public async Task<IEnumerable<TResult>> GetProjectedAsync<TGroupKey, TResult>(
            IProjectionSpecification<TEntity, TKey, TGroupKey, TResult> spec)
            where TResult : class
        {
            var baseQuery = dbContext.Set<TEntity>().AsQueryable();

            var filteredQuery = spec.Criteria is not null
                ? baseQuery.Where(spec.Criteria)
                : baseQuery;

            var projectedQuery = filteredQuery
                .GroupBy(spec.GroupBy!)
                .Select(spec.SelectProjection!);

            return await projectedQuery.ToListAsync();
        }

        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);

        public void UpdateRange(IEnumerable<TEntity> entities) => dbContext.Set<TEntity>().UpdateRange(entities); //it will be for upload bulk methods

        public void Delete(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);

        public void DeleteRange(IEnumerable<TEntity> entities) => dbContext.Set<TEntity>().RemoveRange(entities); //it will be for upload bulk methods


        //helper method
        private IQueryable<TEntity> Evaluator(ISpecifications<TEntity, TKey> specs)
           => SpecificationEvaluator.CreateQuery<TEntity, TKey>(dbContext.Set<TEntity>(), specs);

    }
}
