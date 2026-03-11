using Karim.Customer.HrApplication.Domain.Entities.Attendance;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.FingerprintConfiguration
{
    public class FingerprintConfigurations : BaseAuditableEntityConfiguration<Fingerprint, string>
    {
        public override void Configure(EntityTypeBuilder<Fingerprint> builder)
        {
            base.Configure(builder);
            builder.Property(FP => FP.CheckIn).HasColumnType("time").IsRequired();
            builder.Property(FP => FP.CheckOut).HasColumnType("time").IsRequired(false);
            builder.Property(FP => FP.Date).HasColumnType("date").IsRequired();
            builder.Property(FP => FP.DurationInHours).IsRequired(false);
            builder.Property(FP => FP.Long).HasColumnType("decimal(10,7)").IsRequired();
            builder.Property(FP => FP.Lat).HasColumnType("decimal(10,7)").IsRequired();
            builder.Property(FP => FP.Status).HasConversion(
                (status) => status.ToString(),
                (status) => (FingerprintStatus)Enum.Parse(typeof(FingerprintStatus), status)
                ).IsRequired();

            //relationships
            //Employee relationship
            builder.HasOne(FB => FB.Employee)
                .WithMany(E => E.FingerprintLog)
                .HasForeignKey(FB => FB.EmpId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
