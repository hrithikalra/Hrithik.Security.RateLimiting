using Hrithik.Security.RateLimiting.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Application builder extensions for rate limiting.
    /// </summary>
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds the rate limiting middleware to the pipeline.
        /// </summary>
        public static IApplicationBuilder UseHrithikRateLimiting(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<RateLimitingMiddleware>();
        }
    }
}
