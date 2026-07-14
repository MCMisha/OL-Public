using WebApplicationOperaLublin.Exceptions;

namespace WebApplicationOperaLublin.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
        catch (InvalidArtistCategoryException ex)
        {
            _logger.LogWarning(ex, "Artist validation error");
            await HandleExceptionAsync(context, ex.Message, StatusCodes.Status400BadRequest);
        }
        catch (PerformanceNotFoundException ex)
        {
            _logger.LogWarning(ex, "Performance not found");
            await HandleExceptionAsync(context, ex.Message, StatusCodes.Status404NotFound);
        }
        catch (ImplementerNotFoundException ex)
        {
            _logger.LogWarning(ex, "Implementer not found");
            await HandleExceptionAsync(context, ex.Message, StatusCodes.Status404NotFound);
        }
        catch (PerformanceEventNotFoundException ex)
        {
            _logger.LogWarning(ex, "Performance event not found");
            await HandleExceptionAsync(context, ex.Message, StatusCodes.Status404NotFound);
        }
        catch (AboutSectionNotFoundException ex)
        {
            _logger.LogWarning(ex, "About section not found");
            await HandleExceptionAsync(context, ex.Message, StatusCodes.Status404NotFound);
        }
        catch (ArtistNotFoundException ex)
        {
            _logger.LogWarning(ex, "Artist not found");
            await HandleExceptionAsync(context, ex.Message, StatusCodes.Status404NotFound);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Key not found");
            await HandleExceptionAsync(context, ex.Message, StatusCodes.Status400BadRequest);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Validation error");
            await HandleExceptionAsync(context, ex.Message, StatusCodes.Status400BadRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled error");
            await HandleExceptionAsync(context, "Internal server error", StatusCodes.Status500InternalServerError);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, string message, int code)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;

        var response = new { error = message };

        return context.Response.WriteAsJsonAsync(response);
    }
}
