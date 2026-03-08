using Karim.Customer.HrApplication.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.AppUserConfiguration
{
    public class AppUserConfigurations : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            //builder.Property(U => U.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(U => U.DisplayName).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(U => U.isSuspended).IsRequired(true);
            builder.Property(U => U.LastLoginDate).IsRequired(false);
            builder.Property(U => U.CreatedBy).IsRequired(false);
            builder.Property(U => U.CreatedOn).IsRequired(false);
            builder.Property(U => U.ModifiedBy).IsRequired(false);
            builder.Property(U => U.ModifiedOn).IsRequired(false);
            builder.Property(U => U.RemovedBy).IsRequired(false);
            builder.Property(U => U.RemovedOn).IsRequired(false);
            builder.Property(U => U.isRemoved).IsRequired(true);
            //Relationship between AppUser and Employee
            builder.HasOne(U => U.Employee)
                   .WithOne(E => E.Account)
                   .HasForeignKey<AppUser>(U => U.EmpId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
