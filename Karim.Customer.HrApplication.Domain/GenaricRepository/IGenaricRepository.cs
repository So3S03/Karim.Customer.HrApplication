using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;

namespace Karim.Customer.HrApplication.Domain.GenaricRepository
{
    public interface IGenaricRepository<TEntity, TKey> 
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        IQueryable<TEntity> GetAllQueryable();
        Task<IEnumerable<TEntity>> GetAllAsync(bool AsNoTraking);
        Task<TEntity?> GetById(TKey id);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities); //it will be for upload bulk methods
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities); //it will be for upload bulk methods
        void Delete(TKey id);
        void DeleteRange(IEnumerable<TEntity> entities); //it will be for upload bulk methods
    }
}
