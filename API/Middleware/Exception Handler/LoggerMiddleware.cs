using BLL.Infrastructure.Logger;

namespace API.Middleware.Exception_Handler
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;
        public LoggerMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            _logger.LogInfo(await FormatRequest(httpContext.Request));
            Stream originalBody = httpContext.Response.Body;
            using (var memStream = new MemoryStream())
            {
                httpContext.Response.Body = memStream;

                await _next(httpContext);

                memStream.Position = 0;
                string responseBody = new StreamReader(memStream).ReadToEnd();

                memStream.Position = 0;
                await memStream.CopyToAsync(originalBody);
                httpContext.Response.Body = originalBody;
                _logger.LogInfo("Responce " + httpContext.Response.StatusCode + " " + responseBody);
            }
        }
        private async Task<string> FormatRequest(HttpRequest request)
        {
            HttpRequestRewindExtensions.EnableBuffering(request);

            using var streamReader = new StreamReader(request.Body, leaveOpen: true);
            var requestBody = await streamReader.ReadToEndAsync();

            request.Body.Position = 0;

            return $"Request {request.Scheme} {request.Host} {request.Path} {request.QueryString} {requestBody.Replace('\n', ' ')}";
        }
    }
}
