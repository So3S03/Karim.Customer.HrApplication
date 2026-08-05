using Karim.Customer.HrApplication.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;

namespace Karim.Customer.HrApplication.APIs.ErrorHandeler
{
    public class ErrorHandlerMiddleware
    {
        public RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
             _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Determine the correct status code first
                var statusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    ConflictException => StatusCodes.Status409Conflict,
                    UnauthorizedException => StatusCodes.Status401Unauthorized,
                    ForbiddenException => StatusCodes.Status403Forbidden,
                    MethodNotAllowedException => StatusCodes.Status405MethodNotAllowed,
                    _ => StatusCodes.Status500InternalServerError
                };

                // Set the status code ONCE with the correct value
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                // Log the error
                Log.Error(ex, $"Error on path {context.Request.Path}: {ex.Message}", context.Request.Path, ex.Message);

                // Create problem details
                var problem = new ProblemDetails()
                {
                    Title = ex switch
                    {
                        NotFoundException => "Resource You Try To Access is Not Found",
                        BadRequestException => "Bad Request",
                        ConflictException => "An Conflict Happend",
                        UnauthorizedException => "You Are Not Authorized To Do This Action",
                        ForbiddenException => "Forbidden",
                        MethodNotAllowedException => "This EndPoint Can't be Accessed Using This HTTP Method",
                        _ => "Something Went Wrong!"
                    },
                    Detail = ex.Message,
                    Instance = context.Request.Path,
                    Status = statusCode  // This should match context.Response.StatusCode
                };

                await context.Response.WriteAsJsonAsync(problem);
            }
        }
    }
}
