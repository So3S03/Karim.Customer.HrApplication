
using Karim.Customer.HrApplication.APIs.Controllers.Assembly;
using Karim.Customer.HrApplication.APIs.ErrorHandeler;
using Karim.Customer.HrApplication.APIs.Extentions;
using Karim.Customer.HrApplication.Application.ApplicationDI;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Contexts;
using Karim.Customer.HrApplication.Infrastructure.Persistence.PersistenceDI;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;
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
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                })
                .AddApplicationPart(typeof(ControllersAssembly).Assembly);
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            //registering Persistence DI
            builder.Services.AddPersistenceDI(builder.Configuration);

            //registering Application DI 
            builder.Services.ApplicationDIContainer();

            //configure validation Errors
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var Errors = actionContext.ModelState.Where(E => E.Value.Errors.Count > 0)
                    .ToDictionary(E => E.Key, E => E.Value.Errors.Select(E => E.ErrorMessage));
                    var Problem = new ProblemDetails()
                    {
                        Title = "Validation Error",
                        Detail = "One Or More Validation Error Has Occurred",
                        Status = StatusCodes.Status400BadRequest,
                        Extensions = { { "Errors", Errors }}
                    };
                    return new BadRequestObjectResult(Problem);
                };
            });

            //add serilog into DI container
            builder.Services.AddSerilog();
            //creating serilog configurations to read from appsettings
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

            //registering Swagger UI DI
            builder.Services.AddSwaggerGen();
            builder.Services.AddOpenApi();

            #endregion

           var app = builder.Build();

            //Migrate Database
            await app.DbMigrate<HRMSDBContext>();

            #region Middilewares
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
