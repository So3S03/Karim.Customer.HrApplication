using Karim.Customer.HrApplication.Domain.Entities._Common;
using Karim.Customer.HrApplication.Domain.Entities.Projects;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.ProjectConfiguration
{
    public class ProjectConfigurations : BaseAuditableEntityConfiguration<Project, string>
    {
        public override void Configure(EntityTypeBuilder<Project> builder)
        {
            base.Configure(builder);
            builder.Property(P => P.ProjectCode).HasColumnType("nvarchar(max)").IsRequired(true);
            builder.Property(P => P.ProjectName).HasColumnType("nvarchar(max)").IsRequired(true);
            builder.Property(P => P.Description).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(P => P.ProjectType).HasConversion(
                    (type) => type.ToString(),
                    (type) => (ProjectType)Enum.Parse(typeof(ProjectType), type)
                ).IsRequired(true);
            builder.Property(P => P.ProjectStatus).HasConversion(
                    (status) => status.ToString(),
                    (status) => (ProjectStatus)Enum.Parse(typeof(ProjectStatus), status)
                ).IsRequired(true);
            builder.Property(P => P.ActivatedAt).HasColumnType("datetime2").IsRequired(false);
            builder.Property(P => P.CompletedAt).HasColumnType("datetime2").IsRequired(false);
            builder.Property(P => P.CanceledAt).HasColumnType("datetime2").IsRequired(false);
            builder.Property(P => P.CancelationReason).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(P => P.ProjectCoast).HasColumnType("decimal(15,3)").IsRequired(true);
            builder.Property(P => P.HoursAmount).HasColumnType("decimal(7,2)").IsRequired(true);
            builder.Property(P => P.CoastCurrency).HasConversion(
                    (crncy) => crncy.ToString(),
                    (crncy) => (Currancies)Enum.Parse(typeof(Currancies), crncy)
                ).IsRequired(true);

            //relations
            builder.HasOne(P => P.Department).WithMany(D => D.Projects).HasForeignKey(P => P.DepartmentId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(P => P.Contract).WithOne(C => C.Project)
                .HasForeignKey<Project>(C => C.ContractId).OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(P => P.Tasks).WithOne(T => T.Project).HasForeignKey(T => T.ProjectId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
