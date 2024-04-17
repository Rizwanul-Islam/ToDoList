using Newtonsoft.Json;
using System.Net;
using ToDoService.Application.Common.Exceptions;
using ToDoService.Domain.Exceptions;

namespace ToDoService.Api.Middlewares;

/// <summary>
/// Middleware to handle exceptions that occur while processing http requests.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        string result = JsonConvert.SerializeObject(new ErrorDeatils
        {
            ErrorMessage = exception.Message,
            ErrorType = "Failure"
        });

        switch (exception)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                break;
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(validationException.Errors);
                break;
            case DomainExceptions DomainValidationException:
                statusCode = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(DomainValidationException.Errors);
                break;

            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                break;
            default:
                break;
        }

        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(result);
    }
    public class ErrorDeatils
    {
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
    }

}
