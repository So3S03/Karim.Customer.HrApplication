using Karim.Customer.HrApplication.Domain.Entities.Identity;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.RefreshTokenConfiguration
{
    public class RefreshTokenConfigurations : BaseEntityConfiguration<RefreshToken, string>
    {
        public override void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            base.Configure(builder);
            builder.Property(rt => rt.TokenHash).HasMaxLength(500).IsRequired();
            builder.HasIndex(rt => rt.TokenHash).IsUnique();
            builder.Property(rt => rt.ExpiryDate).HasColumnType("datetime2").IsRequired();
            builder.Property(rt => rt.CreatedAt).HasColumnType("datetime2").IsRequired();
            builder.HasOne(rt => rt.User).WithMany(u => u.RefreshTokens).HasForeignKey(rt => rt.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
