using Microsoft.AspNetCore.Mvc;

namespace WebApi.Middleware
{
    class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        public GlobalExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                if (exception is TaskCanceledException)
                {
                    _logger.LogWarning("task canceled: {ex}", exception);
                    context.Response.StatusCode = 499;
                }
                else
                {
                    _logger.LogError("error occurred!!! {ex}", exception);
                    var problem = new ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "server error",
                        Detail = "an internal error has occurred"
                    };
                    context.Response.StatusCode = problem.Status.Value;
                    await context.Response.WriteAsJsonAsync(problem);
                }
            }
        }
    }
}
