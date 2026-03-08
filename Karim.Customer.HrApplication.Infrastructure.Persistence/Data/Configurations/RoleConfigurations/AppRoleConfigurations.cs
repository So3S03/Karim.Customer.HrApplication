using Karim.Customer.HrApplication.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.RoleConfigurations
{
    public class AppRoleConfigurations : IEntityTypeConfiguration<AppPrivilages>
    {
        public void Configure(EntityTypeBuilder<AppPrivilages> builder)
        {
            builder.Property(R => R.PrivNumber).HasPrecision(18, 0).IsRequired();
        }
    }
}
