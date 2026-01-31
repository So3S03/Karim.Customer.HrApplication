using Karim.Customer.HrApplication.Domain.Entities.Departmnet;
using Karim.Customer.HrApplication.Domain.Entities.Employee;
using Karim.Customer.HrApplication.Infrastructure.Persistence._Common;
using Microsoft.EntityFrameworkCore;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Contexts
{
    public class HRMSDBContext(DbContextOptions<HRMSDBContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceLayerAssembly).Assembly); //It May Change In Future Cause There Will be 2 Contexts
        }

        //Departmant Table
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
    }
}
