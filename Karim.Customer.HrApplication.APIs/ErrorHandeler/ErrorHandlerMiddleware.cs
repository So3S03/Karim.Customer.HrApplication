using Karim.Customer.HrApplication.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Karim.Customer.HrApplication.APIs.ErrorHandeler
{
    public class ErrorHandlerMiddleware
    {
        public RequestDelegate _next;
        public ILogger<ErrorHandlerMiddleware> _logger { get; set; }
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
             _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await _next(context);
            }
            catch (Exception ex)
            {
                // change status
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //logging exception (it will be changed to use Srilog package)
                _logger.LogError(ex.Message, ex);
                //return custom view for client
                var problem = new ProblemDetails()
                {
                    Title = ex switch
                    {
                        NotFoundException => "Resource You Try To Access is Not Found",
                        BadRequestException => "Bad Request",
                        UnauthorizedException => "You Are Not Autorized To Do This Action",
                        ForbiddenException => "Forbidden",
                        MethodNotAllowedException => "This EndPoint Can't be Accessed Using This HTTP Method",
                        _ => "Something Went Wrong !"
                    },
                    Detail = ex.Message,
                    Instance = context.Request.Path,
                    Status = ex switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        BadRequestException => StatusCodes.Status400BadRequest,
                        UnauthorizedException => StatusCodes.Status401Unauthorized,
                        ForbiddenException => StatusCodes.Status403Forbidden,
                        MethodNotAllowedException => StatusCodes.Status405MethodNotAllowed,
                        _ => StatusCodes.Status500InternalServerError
                    }
                };
                await context.Response.WriteAsJsonAsync(problem);
            }
        }
    }
}
