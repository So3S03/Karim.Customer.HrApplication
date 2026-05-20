using Karim.Customer.HrApplication.Domain.Entities.Tasks;
using status =  Karim.Customer.HrApplication.Domain.Entities.Tasks.TaskStatus;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.TasksConfiguration
{
    public class TasksConfigurations : BaseEntityConfiguration<Tasks, string>
    {
        public override void Configure(EntityTypeBuilder<Tasks> builder)
        {
            base.Configure(builder);
            builder.Property(T => T.Code).HasColumnType("nvarchar(max)").IsRequired(true);
            builder.Property(T => T.Name).HasColumnType("nvarchar(max)").IsRequired(true);
            builder.Property(T => T.Description).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(T => T.StartDate).HasColumnType("date").IsRequired(true);
            builder.Property(T => T.EndDate).HasColumnType("date").IsRequired(true);
            builder.Property(T => T.TaskHours).HasColumnType("decimal(6,2)").IsRequired(true);
            builder.Property(T => T.WorkedHours).HasColumnType("decimal(6,2)").IsRequired(false);
            builder.Property(T => T.LastUsedHours).HasColumnType("decimal(6,2)").IsRequired(false);
            builder.Property(T => T.RemainingHours).HasColumnType("decimal(6,2)").IsRequired(true);
            builder.Property(T => T.LastPull).IsRequired(false);
            builder.Property(T => T.isArchived).IsRequired(true);
            builder.Property(T => T.Status).HasConversion(
                s => s.ToString(),
                s => (status)Enum.Parse(typeof(status), s)
                ).IsRequired(true);
            builder.Property(T => T.Type).HasConversion(
                s => s.ToString(),
                s => (TaskType)Enum.Parse(typeof(TaskType), s)
                ).IsRequired(true);
            builder.HasOne(T => T.Project).WithMany(P => P.Tasks).HasForeignKey(T => T.ProjectId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(T => T.Ticket).WithMany(T => T.Tasks).HasForeignKey(T => T.TicketId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(T => T.Employee).WithMany(E => E.Tasks).HasForeignKey(T => T.EmployeeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
