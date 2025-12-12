using Karim.Customer.HrApplication.Domain.Entities.Department;
using Karim.Customer.HrApplication.Domain.Entities.Departmnet;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.DepartmentConfiguration
{
    public class DepartmentConfiguration : BaseAuditableEntityConfiguration<Department, string>
    {
        public override void Configure(EntityTypeBuilder<Department> builder)
        {
            base.Configure(builder);
            builder.Property(D => D.DepartmentCode).HasColumnType("nvarchar").HasMaxLength(7); //It Should Be Have Value Like DEPT001
            builder.Property(D => D.DepartmentName).HasColumnType("nvarchar").HasMaxLength(100); //It Should Be Have Value Like Front-End / Back-End / Mobile
            builder.Property(D => D.Description).HasColumnType("nvarchar(max)");
            builder.Property(D => D.isActive).IsRequired();
            builder.Property(D => D.isRemoved).IsRequired();
            builder.Property(D => D.ActualCreationDate).HasColumnType("datetime2").IsRequired();
            builder.Property(D => D.DepartmentPhotoUrl).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(D => D.TotalDepartmentBudget).HasPrecision(22,2).IsRequired(true);
            builder.Property(D => D.DepartmentBudgetForSalaries).HasPrecision(22, 2).IsRequired(true);
            builder.Property(D => D.DepartmentBudgetForTools).HasPrecision(22, 2).IsRequired(false);
            builder.Property(D => D.DepartmentBudgetForTrainees).HasPrecision(22, 2).IsRequired(false);
            builder.Property(D => D.DepartmentBudgetOther).HasPrecision(22, 2).IsRequired(false);
            builder.Property(D => D.DepatrmentType).HasConversion(
                (dt) => dt.ToString(),
                (dt) => (DepartmentType)Enum.Parse(typeof(DepartmentType), dt)
                );

        }
    }
}
