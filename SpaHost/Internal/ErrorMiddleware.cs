using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

/// <summary>
/// Class derived from https://www.devtrends.co.uk/blog/handling-errors-in-asp.net-core-web-api ErrorWrappingMiddleware
/// </summary>
namespace SpaHost.Internal
{
    public class ErrorMiddleware
    {
        private readonly ILogger<ErrorMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next,
            ILogger<ErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;
            }

            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";

                var response = new { ErrorCode = context.Response.StatusCode };

                var json = JsonConvert.SerializeObject(response);

                await context.Response.WriteAsync(json);
            }
        }
    }
}