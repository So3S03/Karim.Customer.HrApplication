
using Karim.Customer.HrApplication.APIs.Extentions;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Contexts;
using System.Threading.Tasks;

namespace Karim.Customer.HrApplication.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Dependancy Injection Container
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            #endregion

           var app = builder.Build();

            //Migrate Database
            await app.DbMigrate<HRMSDBContext>();

            #region Middilewares
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
