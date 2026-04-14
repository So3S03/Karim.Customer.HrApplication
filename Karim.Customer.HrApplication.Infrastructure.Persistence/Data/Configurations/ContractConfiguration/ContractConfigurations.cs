using Karim.Customer.HrApplication.Domain.Entities._Common;
using Karim.Customer.HrApplication.Domain.Entities.Contracts;
using Karim.Customer.HrApplication.Domain.Entities.Employee;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Configurations.ContractConfiguration
{
    public class ContractConfigurations : BaseEntityConfiguration<Contract, string>
    {
        public override void Configure(EntityTypeBuilder<Contract> builder)
        {
            base.Configure(builder);
            builder.Property(C => C.ContractCode).HasColumnType("nvarchar(max)").IsRequired(true);
            builder.Property(C => C.ContractType).HasConversion(
                (c) => c.ToString(),
                (c) => (ContractType)Enum.Parse(typeof(ContractType), c)
                ).IsRequired(true);
            builder.Property(C => C.ContractStatus).HasConversion(
                (c) => c.ToString(),
                (c) => (ContractStatus)Enum.Parse(typeof(ContractStatus), c)
                ).IsRequired(true);
            builder.Property(C => C.ContractDate).HasColumnType("date").IsRequired(true);
            builder.Property(C => C.StartDate).HasColumnType("date").IsRequired(true);
            builder.Property(C => C.EndDate).HasColumnType("date").IsRequired(true);
            builder.Property(C => C.EmployeerCompanyName).HasColumnType("nvarchar(max)").IsRequired(true);
            builder.Property(C => C.CompanyRepresentativeName).HasColumnType("nvarchar(max)").IsRequired(true);
            builder.Property(C => C.ContractEmployeeName).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(C => C.NationalId).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(C => C.JobTitle).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(C => C.EmployeeWorkType).HasConversion(
                (cwt) => cwt.ToString(),
                (cwt) => (WorkType)Enum.Parse(typeof(WorkType), cwt)
                ).IsRequired(false);
            builder.Property(C => C.WorkLocation).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(C => C.EmpSalary).HasColumnType("decimal(10,3)").IsRequired(false);
            builder.Property(C => C.ContractorName).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(C => C.ContractorScopOfWork).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(C => C.ContractValue).HasColumnType("decimal(18,3)").IsRequired(false);
            builder.Property(C => C.PaymentTerm).HasConversion(
                (cpt) => cpt.ToString(),
                (cpt) => (PaymentTerm)Enum.Parse(typeof(PaymentTerm), cpt)
                ).IsRequired(false);
            builder.Property(C => C.CommercialRegistrationNumber).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(C => C.CurrencyType).HasConversion(
                (cc) => cc.ToString(),
                (cc) => (Currancies)Enum.Parse(typeof(Currancies), cc)
                ).IsRequired(true);
            builder.Property(C => C.TermsAndConditions).HasColumnType("nvarchar(max)").IsRequired(false);

            //relations
            builder.HasOne(C => C.Employee).WithOne(E => E.Contract)
                .HasForeignKey<Contract>(C => C.EmpId).OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(C => C.Project).WithOne(P => P.Contract)
                .HasForeignKey<Contract>(C => C.ProjectId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
