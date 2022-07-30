namespace Real.Time.Chat.Api.Middlewares
{
    public class RequestHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestHandlerMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                Console.WriteLine($"Request received - ID: {httpContext.Connection.Id} - IP: {httpContext.Connection.RemoteIpAddress}");
                await _next(httpContext);
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    $"Something went wrong with Request ID: {httpContext.Connection.Id} - IP: {httpContext.Connection.RemoteIpAddress}"
                    + $" - StatusCode: {httpContext.Response.StatusCode} - Error: {e.Message}"
                );
            }
        }

    }
}
