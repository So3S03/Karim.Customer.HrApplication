
using Karim.Customer.HrApplication.APIs.Controllers.Assembly;
using Karim.Customer.HrApplication.APIs.Extentions;
using Karim.Customer.HrApplication.Application.ApplicationDI;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Contexts;
using Karim.Customer.HrApplication.Infrastructure.Persistence.PersistenceDI;
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

            builder.Services.AddControllers()
                .AddApplicationPart(typeof(ControllersAssembly).Assembly);
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            //registering Persistence DI
            builder.Services.AddPersistenceDI(builder.Configuration);

            //registering Application DI 
            builder.Services.ApplicationDIContainer();


            //registering Swagger UI DI
            builder.Services.AddSwaggerGen();
            builder.Services.AddOpenApi();
            #endregion

           var app = builder.Build();

            //Migrate Database
            await app.DbMigrate<HRMSDBContext>();

            #region Middilewares
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
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
