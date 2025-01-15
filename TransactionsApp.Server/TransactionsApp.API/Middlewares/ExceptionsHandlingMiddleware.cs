namespace TransactionsApp.API.Middlewares
{
    /// <summary>
    /// Handles exceptions that occur during request processing and returns a response with the appropriate status code.
    /// </summary>
    public class ExceptionsHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsHandlingMiddleware> _logger;

        public ExceptionsHandlingMiddleware(RequestDelegate next, ILogger<ExceptionsHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Processes the request and catches exceptions that occur during request processing.
        /// </summary>
        /// <param name="context">HTTP request context</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;

                await context.Response.WriteAsync("Error occured: " + ex.Message);
            }
        }
    }
}
