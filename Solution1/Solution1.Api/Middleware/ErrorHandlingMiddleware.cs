using System.Text.Json;

namespace Solution1.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger
    )
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var response = CreateResponse(exception, statusCode);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        if (statusCode >= 500)
            _logger.LogError(exception, "Erro interno do servidor: {Message}", exception.Message);
        else if (statusCode >= 400) _logger.LogWarning(exception, "Erro de cliente: {Message}", exception.Message);

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private int GetStatusCode(Exception exception)
    {
        return exception switch
        {
            ArgumentException => StatusCodes.Status400BadRequest,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            InvalidOperationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private object CreateResponse(Exception exception, int statusCode)
    {
        return new
        {
            status = statusCode,
            message = exception.Message,
            detail = exception.InnerException?.Message
        };
    }
}

public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}