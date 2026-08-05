using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.GenaricRepository;

namespace Karim.Customer.HrApplication.Domain.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenaricRepository<TEntity, TKey> GenerateRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey> 
            where TKey : IEquatable<TKey>;

        Task<int> CompleteAsync();
    }
}
