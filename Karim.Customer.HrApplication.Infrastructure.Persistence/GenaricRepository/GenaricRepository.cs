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

        public async Task AddAsync(TEntity entity) => await dbContext.Set<TEntity>().AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await dbContext.Set<TEntity>().AddRangeAsync(entities); //it will be for upload bulk methods

        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);

        public void UpdateRange(IEnumerable<TEntity> entities) => dbContext.Set<TEntity>().UpdateRange(entities); //it will be for upload bulk methods

        public void Delete(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);

        public void DeleteRange(IEnumerable<TEntity> entities) => dbContext.Set<TEntity>().RemoveRange(entities); //it will be for upload bulk methods


        //helper method
        private IQueryable<TEntity> Evaluator(ISpecifications<TEntity, TKey> specs)
           => SpecificationEvaluator.CreateQuery<TEntity, TKey>(dbContext.Set<TEntity>(), specs);
    }
}
