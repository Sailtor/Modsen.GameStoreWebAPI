using BLL.Infrastructure.Logger;
using System.Text;

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
            /*var originalBodyStream = httpContext.Response.Body;*/

            await _next(httpContext);
            /*
            var responseBody = new MemoryStream();
            httpContext.Response.Body = responseBody;
            */
            _logger.LogInfo(await FormatResponse(httpContext.Response));
            /*await responseBody.CopyToAsync(originalBodyStream);*/
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            HttpRequestRewindExtensions.EnableBuffering(request);

            using var streamReader = new StreamReader(request.Body, leaveOpen: true);
            var requestBody = await streamReader.ReadToEndAsync();

            request.Body.Position = 0;

            return $"Request {request.Scheme} {request.Host} {request.Path} {request.QueryString} {requestBody.Replace('\n', ' ')}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            //response.Body.Seek(0, SeekOrigin.Begin);

            response.Body.Position = 0;

            return $"Response {text}";
        }
    }
}
