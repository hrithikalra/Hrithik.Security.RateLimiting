using Hrithik.Security.RateLimiting.Configuration;
using Microsoft.AspNetCore.Http;

namespace Hrithik.Security.RateLimiting.Abstractions
{
    /// <summary>
    /// Defines how rate limit rules are resolved for a request.
    /// </summary>
    public interface IRateLimitPolicy
    {
        /// <summary>
        /// Resolves the applicable rate limit rule.
        /// </summary>
        /// <param name="context">HTTP context.</param>
        /// <returns>Resolved rate limit rule.</returns>
        RateLimitRule Resolve(HttpContext context);
    }
}
