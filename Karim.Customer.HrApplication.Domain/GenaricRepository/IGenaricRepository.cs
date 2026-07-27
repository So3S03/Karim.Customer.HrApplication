using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.Specifications;
using System.Linq.Expressions;

namespace Karim.Customer.HrApplication.Domain.GenaricRepository
{
    public interface IGenaricRepository<TEntity, TKey> 
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications);
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications);
        Task<TEntity?> GetByIdAsyncWithNoTracking(ISpecifications<TEntity, TKey> specifications);
        Task<int> GetDataCountAsync(ISpecifications<TEntity, TKey> specifications); //Get Count Of Data After Applying Filteration
        Task<decimal> GetDataSumAsync(ISpecifications<TEntity, TKey> specifications, Expression<Func<TEntity, decimal>> sumExprission);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities); //it will be for upload bulk methods
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities); //it will be for upload bulk methods
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities); //it will be for upload bulk methods
        IQueryable<TEntity> GetQuery(ISpecifications<TEntity, TKey> specifications); //For Creating Queries Without Sending Request To Database
    }
}
