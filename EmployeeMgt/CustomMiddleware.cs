using System.Diagnostics;

namespace EmployeeMgt
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddleware> _logger;

        public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                // 👉 Log incoming request
                _logger.LogInformation("Incoming Request: {method} {url}",
                    context.Request.Method,
                    context.Request.Path);

                // Call next middleware
                await _next(context);

                // 👉 Log outgoing response
                _logger.LogInformation("Outgoing Response: {statusCode}",
                    context.Response.StatusCode);
            }
            catch (Exception ex)
            {
                // 👉 Global exception handling
                _logger.LogError(ex, "An unhandled exception occurred while processing request.");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
            }
            finally
            {
                stopwatch.Stop();
                // 👉 Performance monitoring
                _logger.LogInformation("Request processed in {elapsedMilliseconds} ms",
                    stopwatch.ElapsedMilliseconds);
            }
        }


    }
}

