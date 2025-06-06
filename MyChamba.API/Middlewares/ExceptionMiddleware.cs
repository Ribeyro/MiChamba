namespace MyChamba.Middlewares;

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
        catch (Exception ex)
        {
            _logger.LogError($"Error en la petición: {ex}");
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/problem+json";

            var error = new
            {
                type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                title = "Error interno del servidor",
                status = 500,
                detail = ex.Message
            };

            await context.Response.WriteAsJsonAsync(error);
        }
    }
}