namespace Real.Time.Chat.Api.Middlewares
{
    public class ResponseHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseHandlerMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                Console.WriteLine(
                    $"Response completed - ID: {httpContext.Connection.Id} - IP: {httpContext.Connection.RemoteIpAddress}"
                    + $" - StatusCode: {httpContext.Response.StatusCode} - Response: {httpContext.Response.Body.ToString}");
                await _next(httpContext);
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    $"Something went wrong with Response ID: {httpContext.Connection.Id} - IP: {httpContext.Connection.RemoteIpAddress}"
                    + $" - StatusCode: {httpContext.Response.StatusCode} - Error: {e.Message}"
                );
            }
        }

    }
}
