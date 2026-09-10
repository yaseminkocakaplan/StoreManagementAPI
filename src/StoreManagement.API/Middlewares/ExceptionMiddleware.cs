using System.Net;
using StoreManagement.Application.Common;
using StoreManagement.Application.Exceptions;

namespace StoreManagement.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Hata oluştu: {Message}", ex.Message);
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = exception switch
        {
            NotFoundException => HttpStatusCode.NotFound,       // 404
            BadRequestException => HttpStatusCode.BadRequest,   // 400
            _ => HttpStatusCode.InternalServerError             // 500
        };

        context.Response.StatusCode = (int)statusCode;

        // SQL'in asıl derdini (en dipteki gerçek hatayı) yakalıyoruz:
        var actualMessage = exception.InnerException?.InnerException?.Message 
                            ?? exception.InnerException?.Message 
                            ?? exception.Message;

        var response = new ErrorDetails
        {
            StatusCode = context.Response.StatusCode,
            Message = actualMessage,
            Detailed = _env.IsDevelopment() ? exception.StackTrace : null
        };

        await context.Response.WriteAsync(response.ToString());
    }
}