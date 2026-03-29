using Karim.Customer.HrApplication.Domain.Entities.Attendance;
using Karim.Customer.HrApplication.Domain.Entities.BaseEntities;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.RequetConfiguration
{
    public class RequestConfigurations : BaseAuditableEntityConfiguration<Requests, string>
    {
        public override void Configure(EntityTypeBuilder<Requests> builder)
        {
            base.Configure(builder);
            builder.Property(R => R.StartDate).HasColumnType("date").IsRequired();
            builder.Property(R => R.EndDate).HasColumnType("date").IsRequired();
            builder.Property(R => R.Reason).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(R => R.Notes).HasColumnType("nvarchar(max)").IsRequired(false);
            //builder.Property(R => R.Duration).IsRequired(false);
            builder.Property(R => R.ApprovedById).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(R => R.ApprovedByName).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(R => R.RejectedById).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(R => R.RejectedByName).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(R => R.Status).HasConversion(
                (status) => status.ToString(),
                (status) => (RequestStatus)Enum.Parse(typeof(RequestStatus), status)
                ).IsRequired();
            builder.Property(R => R.Type).HasConversion(
                (type) => type.ToString(),
                (type) => (RequestType)Enum.Parse(typeof(RequestType), type)
                ).IsRequired();
            //relationship
            builder.HasOne(R => R.Employee).WithMany(E => E.Requests).HasForeignKey(R => R.EmpId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(R => R.Fingerprint).WithOne(F => F.Request).HasForeignKey<Requests>(R => R.FingerprintId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
