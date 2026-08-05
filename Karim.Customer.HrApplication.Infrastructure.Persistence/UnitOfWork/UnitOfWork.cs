using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Domain.GenaricRepository;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork<TContext>(TContext dbContext) : IUnitOfWork where TContext : DbContext
    {
        private readonly ConcurrentDictionary<string, object> storedRepos = new ConcurrentDictionary<string, object>();
        public IGenaricRepository<TEntity, TKey> GenerateRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
            => (IGenaricRepository<TEntity, TKey>) storedRepos.GetOrAdd(typeof(TEntity).Name, new GenaricRepository.GenaricRepository<TEntity, TKey>(dbContext));

        public async Task<int> CompleteAsync() => await dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync() => await dbContext.DisposeAsync();
    }
}
