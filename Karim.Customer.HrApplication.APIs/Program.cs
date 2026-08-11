
using Hangfire;
using Karim.Customer.HrApplication.APIs.Controllers.Assembly;
using Karim.Customer.HrApplication.APIs.ErrorHandeler;
using Karim.Customer.HrApplication.APIs.Extentions;
using Karim.Customer.HrApplication.Application.ApplicationDI;
using Karim.Customer.HrApplication.Infrastructure.HangfireServices;
using Karim.Customer.HrApplication.Infrastructure.InfraDIContainer;
using Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Contexts;
using Karim.Customer.HrApplication.Infrastructure.Persistence.PersistenceDI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
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

            //registering Infrastructure DI
            builder.Services.AddInfrastructure(builder.Configuration);

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

            //Add Authentication
            builder.Services.AddAuthentication(configOptions =>
            {
                configOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer("Bearer", configOptions =>
                {
                    configOptions.SaveToken = true;
                    var jwtSettings = builder.Configuration.GetSection("JwtConfigs");
                    var secretKey = jwtSettings["SecretKey"];
                    var expireTime = jwtSettings["ExpiringTime"];
                    configOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issure"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
                        ClockSkew = TimeSpan.FromMinutes(int.Parse(expireTime!))
                    };
                });
            //Rate Limiting Configuration For Stop Brute Force Attacks
            builder.Services.AddRateLimiter(RLOptions =>
            {
                RLOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                RLOptions.AddPolicy("SignInPolicy", httpContext =>
                {
                    var remoteIp = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                    return RateLimitPartition.GetSlidingWindowLimiter(remoteIp, _ => new SlidingWindowRateLimiterOptions()
                    {
                        PermitLimit = 5,
                        Window = TimeSpan.FromMinutes(1),
                        SegmentsPerWindow = 4,
                        QueueLimit = 0
                    });
                });
                RLOptions.OnRejected = async (context, cancellationToken) =>
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    await context.HttpContext.Response.WriteAsync("{\"Message\":\"Too many login attempts. Please try again later.\"}", cancellationToken);
                };
            });
            builder.Services.AddAuthorization();
            //registering Swagger UI DI
            builder.Services.AddSwaggerGen();
            builder.Services.AddOpenApi();
            #endregion

           var app = builder.Build();

            
            //Migrate Database
            await app.DbMigrate<HRMSDBContext>();
            app.UseRateLimiter();
            //creating serilog configurations to read from appsettings
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
            using (var scope = app.Services.CreateScope())
            {
                var recuringJob = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
                HangfireServices.HangfireJobs(recuringJob);
            }
            app.UseHangfireDashboard("/HangfireDashboard");

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

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
