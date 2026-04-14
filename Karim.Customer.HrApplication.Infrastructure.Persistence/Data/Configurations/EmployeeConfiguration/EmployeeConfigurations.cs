using Karim.Customer.HrApplication.Domain.Entities.Attendance;
using Karim.Customer.HrApplication.Domain.Entities.Departmnet;
using Karim.Customer.HrApplication.Domain.Entities.Employee;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.EmployeeConfiguration
{
    public class EmployeeConfigurations : BaseAuditableEntityConfiguration<Employee, string>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);
            builder.HasIndex(E => E.EmployeeCode).IsUnique();
            builder.Property(E => E.FullName).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(E => E.PersonalEmail).HasColumnType("nvarchar(max)");
            builder.Property(E => E.WorkEmail).HasColumnType("nvarchar(max)");
            builder.Property(E => E.Position).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(E => E.PhoneNumber).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(E => E.ExtraPhoneNumber).HasColumnType("nvarchar(max)");
            builder.Property(E => E.Address).HasColumnType("nvarchar(max)");
            builder.Property(E => E.PhotoUrl).HasColumnType("nvarchar(max)");
            builder.Property(E => E.WorkType).HasConversion(
                (wt) => wt.ToString(),
                (wt) => (WorkType)Enum.Parse(typeof(WorkType), wt)
                );
            builder.Property(E => E.EmployeeType).HasConversion(
                (et) => et.ToString(),
                (et) => (EmployeeType)Enum.Parse(typeof(EmployeeType), et)
                );
            builder.Property(E => E.WorkLocation).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(E => E.IsHasContract).IsRequired();
            builder.Property(E => E.ContractEndDate).HasColumnType("datetime2");
            builder.Property(E => E.Salary).HasPrecision(22, 2).IsRequired(false);
            builder.Property(E => E.JoinDate).HasColumnType("datetime2").IsRequired();
            builder.Property(E => E.EmployeeStatus).HasConversion(
                (es) => es.ToString(),
                (es) => (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), es)
                ).IsRequired(false);
            builder.Property(E => E.Rank).HasConversion(
                (er) => er.ToString(),
                (er) => (EmployeeRank)Enum.Parse(typeof(EmployeeRank), er)
                ).IsRequired();
            //Relationships
            //Employee has One Department & Department Has Many Employees
            builder.HasOne(E => E.Department).WithMany(D => D.Employees)
                .HasForeignKey(E => E.DepartmentId).OnDelete(DeleteBehavior.SetNull);
            //Department Only Has One Manager & Manager Only Have One Managed Department
            builder.HasOne(E => E.ManagedDepartment).WithOne(D => D.Manager)
                .HasForeignKey<Department>(D => D.ManagerId).OnDelete(DeleteBehavior.SetNull);
            //Account Relationship
             builder.HasOne(E => E.Account).WithOne(A => A.Employee)
                .HasForeignKey<Employee>(E => E.AccountId).OnDelete(DeleteBehavior.Cascade);
            //Fingerprints
            builder.HasMany(E => E.FingerprintLog).WithOne(FB => FB.Employee)
                .HasForeignKey(FB => FB.EmpId).OnDelete(DeleteBehavior.Cascade);
            //Contract
            builder.HasOne(E => E.Contract).WithOne(C => C.Employee)
                .HasForeignKey<Employee>(E => E.ContractId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
