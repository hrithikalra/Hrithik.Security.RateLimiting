using Hrithik.Security.RateLimiting.Configuration;
using Hrithik.Security.RateLimiting.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Hrithik.Security.RateLimiting.Middleware
{
    /// <summary>
    /// ASP.NET Core middleware for enforcing rate limits.
    /// </summary>
    public sealed class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SlidingWindowLimiter _limiter;
        private readonly RateLimitOptions _options;

        /// <summary>
        /// Initializes a new instance of <see cref="RateLimitingMiddleware"/>.
        /// </summary>
        public RateLimitingMiddleware(
            RequestDelegate next,
            SlidingWindowLimiter limiter,
            IOptions<RateLimitOptions> options)
        {
            _next = next;
            _limiter = limiter;
            _options = options.Value;
        }

        /// <summary>
        /// Processes the HTTP request.
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            var identity =
                context.Items["ApiKeyId"]?.ToString() ??
                context.Connection.RemoteIpAddress?.ToString() ??
                "anonymous";

            var endpointKey = $"{context.Request.Method}:{context.Request.Path}";
            var rule = _options.EndpointRules.TryGetValue(endpointKey, out var r)
                ? r
                : _options.GlobalRules.FirstOrDefault();

            if (rule is null)
            {
                await _next(context);
                return;
            }

            var result = await _limiter.CheckAsync($"{identity}:{endpointKey}", rule);

            if (!result.IsAllowed)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.Response.Headers["Retry-After"] =
                    ((int)result.RetryAfter.TotalSeconds).ToString();
                var payload = JsonSerializer.Serialize(new
                {
                    error = "rate_limit_exceeded",
                    retryAfter = (int)result.RetryAfter.TotalSeconds
                });

                await context.Response.WriteAsync(payload);

                return;
            }

            await _next(context);
        }
    }
}
