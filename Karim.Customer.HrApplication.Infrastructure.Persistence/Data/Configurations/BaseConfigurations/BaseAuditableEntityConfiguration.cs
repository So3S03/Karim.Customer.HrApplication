using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.BaseConfigurations
{
    public class BaseAuditableEntityConfiguration<TEntity, TKey> : BaseEntityConfiguration<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey> //it will work cause baseAuditableEntity inherit from baseEntity
        where TKey : IEquatable<TKey>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
            builder.Property(AE => AE.CreatedBy).IsRequired(); // rest of handling will be in interceptor
            builder.Property(AE => AE.CreatedOn).IsRequired(); // rest of handling will be in interceptor
            builder.Property(AE => AE.ModifiedBy).IsRequired();// rest of handling will be in interceptor
            builder.Property(AE => AE.ModifiedOn).IsRequired();// rest of handling will be in interceptor
        }
    }
}
