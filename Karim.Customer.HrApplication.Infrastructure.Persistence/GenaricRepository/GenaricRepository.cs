using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.GenaricRepository;
using Microsoft.EntityFrameworkCore;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.GenaricRepository
{
    public class GenaricRepository<TEntity, TKey>(DbContext dbContext) : IGenaricRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public IQueryable<TEntity> GetAllQueryable() => dbContext.Set<TEntity>().AsQueryable();

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool AsNoTracking) => await (AsNoTracking ? dbContext.Set<TEntity>().AsNoTracking().ToListAsync() : dbContext.Set<TEntity>().ToListAsync());

        public async Task<TEntity?> GetById(TKey id) => await dbContext.Set<TEntity>().FindAsync(id);

        public async Task AddAsync(TEntity entity) => await dbContext.Set<TEntity>().AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await dbContext.AddRangeAsync(entities); //it will be for upload bulk methods

        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);

        public void UpdateRange(IEnumerable<TEntity> entities) => dbContext.UpdateRange(entities); //it will be for upload bulk methods

        public void Delete(TKey id) => dbContext.Remove(id);

        public void DeleteRange(IEnumerable<TEntity> entities) => dbContext.RemoveRange(entities); //it will be for upload bulk methods
    }
}
