using Karim.Customer.HrApplication.Domain.Entities.Payroll;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.PayslipConfiguration
{
    public class PayslipConfiguration : BaseEntityConfiguration<Payslip, string>
    {
        public override void Configure(EntityTypeBuilder<Payslip> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.StartDate).HasColumnType("date").IsRequired();
            builder.Property(p => p.EndDate).HasColumnType("date").IsRequired();
            builder.Property(p => p.BasicSalary).HasColumnType("decimal(14, 2)").IsRequired();
            builder.Property(p => p.TotalOvertime).HasColumnType("decimal(8, 2)").IsRequired(false);
            builder.Property(p => p.NetSalary).HasColumnType("decimal(14, 2)").IsRequired();
            builder.Property(p => p.PaidAt).HasColumnType("datetime2").IsRequired(false);
            builder.Property(p => p.PaidNotes).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(p => p.Status).HasConversion(
                (s) => s.ToString(),
                (s) => (PayrollStatus)Enum.Parse(typeof(PayrollStatus), s)
                ).IsRequired();
            builder.Property(p => p.PaymentWay).HasConversion(
                (s) => s.ToString(),
                (s) => (PayrollPaymentWay)Enum.Parse(typeof(PayrollPaymentWay), s)
                ).IsRequired(false);
            //Relations
            builder.HasOne(p => p.Employee)
                .WithMany(e => e.Payslips)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.PayrollBonuses)
                .WithOne(pb => pb.Payslip)
                .HasForeignKey(pb => pb.PayslipId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.PayrollPenalties)
                .WithOne(pb => pb.Payslip)
                .HasForeignKey(pb => pb.PayslipId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.PayrollAllowances)
                .WithOne(pb => pb.Payslip)
                .HasForeignKey(pb => pb.PayslipId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
