using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Services.Shared.Web.Middleware
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggerMiddleware> _logger;

        public RequestLoggerMiddleware(RequestDelegate next, ILogger<RequestLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"[Request Started]: {context.Request.Host}{context.Request.Path}");

            try
            {
                await _next(context);
            }
            finally
            {
                _logger.LogInformation($"[Request Ended]: {context.Request.Path}");
            }
        }
    }
}
