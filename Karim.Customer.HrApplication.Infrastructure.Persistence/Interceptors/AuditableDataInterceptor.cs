using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Shared._Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Interceptors
{
    internal class AuditableDataInterceptor(ILoggedInUserService _loggedInUserService) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateAuditFields(eventData.Context);
            return base.SavingChanges(eventData, result);
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateAuditFields(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        private void UpdateAuditFields(DbContext? dbContext)
        {
            if (dbContext is null) return;
            var entries = dbContext.ChangeTracker.Entries().Where(E => E.Entity is BaseAuditableEntity<string> &&
            (E.State == EntityState.Added || E.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                var entity = entry.Entity as BaseAuditableEntity<string>;
                if(entry.State == EntityState.Added)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                    entity.CreatedBy = _loggedInUserService.UserId!;
                }
                else if(entry.State == EntityState.Modified && entity.isRemoved == false)
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                    entity.ModifiedBy = _loggedInUserService.UserId!;
                }
                else if (entry.State == EntityState.Modified && entity.isRemoved == true)
                {
                    entity.RemovedOn = DateTime.UtcNow;
                    entity.RemovedBy = _loggedInUserService.UserId!;
                }
            }
        }
    }
}
