using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.Specifications;

namespace Karim.Customer.HrApplication.Domain.GenaricRepository
{
    public interface IGenaricRepository<TEntity, TKey> 
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications);
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications);
        Task<int> GetDataCountAsync(ISpecifications<TEntity, TKey> specifications); //Get Count Of Data After Applying Filteration
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities); //it will be for upload bulk methods
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities); //it will be for upload bulk methods
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities); //it will be for upload bulk methods
    }
}
