using Karim.Customer.HrApplication.Domain.Entities.Payroll;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.PenaltyConfiguration
{
    public class PenaltyConfiguration : BaseEntityConfiguration<PayrollPenalty, string>
    {
        public override void Configure(EntityTypeBuilder<PayrollPenalty> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Title).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(p => p.Value).HasColumnType("decimal(8, 2)").IsRequired();
            builder.Property(p => p.Description).HasColumnType("nvarchar(max)").IsRequired(false);
                builder.HasOne(p => p.Payslip)
                    .WithMany(p => p.PayrollPenalties)
                    .HasForeignKey(p => p.PayslipId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
