using Karim.Customer.HrApplication.Domain.Entities.Attendance;
using Karim.Customer.HrApplication.Domain.Entities.Departmnet;
using Karim.Customer.HrApplication.Domain.Entities.Employee;
using Karim.Customer.HrApplication.Domain.Entities.Identity;
using Karim.Customer.HrApplication.Infrastructure.Persistence._Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Contexts
{
    public class HRMSDBContext(DbContextOptions<HRMSDBContext> options) : IdentityDbContext<AppUser, AppPrivilages, string>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceLayerAssembly).Assembly); //It May Change In Future Cause There Will be 2 Contexts
        }

        //Departmant Table
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Fingerprint> Fingerprint { get; set; }
    }
}
