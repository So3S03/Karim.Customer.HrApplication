using Karim.Customer.HrApplication.Domain.Entities.Tickets;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.TicketsConfiguration
{
    public class TicketConfigurations : BaseEntityConfiguration<Ticket, string>
    {
        public override void Configure(EntityTypeBuilder<Ticket> builder)
        {
            base.Configure(builder);
            builder.Property(T => T.TicketCode).HasColumnType("nvarchar(max)").IsRequired(true);
            builder.Property(T => T.Name).HasColumnType("nvarchar(max)").IsRequired(true);
            builder.Property(T => T.NormalizedName).HasColumnType("nvarchar(max)").IsRequired(true);
            builder.Property(T => T.Description).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(T => T.HoursNumber).HasColumnType("decimal(7,2)").IsRequired(true);
            builder.Property(T => T.StartDate).IsRequired(true);
            builder.Property(T => T.EndDate).IsRequired(true);
            builder.Property(T => T.IsArchive).IsRequired(true);
            builder.Property(T => T.Status).HasConversion(
                s => s.ToString(),
                s => (TicketStatus)Enum.Parse(typeof(TicketStatus), s)
                ).IsRequired(true);
            builder.HasOne(T => T.Project).WithMany(P => P.Tickets)
                .HasForeignKey(p => p.ProjectId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(T => T.Tasks).WithOne(T => T.Ticket).HasForeignKey(T => T.TicketId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
