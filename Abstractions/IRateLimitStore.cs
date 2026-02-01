using Hrithik.Security.RateLimiting.Core;

namespace Hrithik.Security.RateLimiting.Abstractions
{
    /// <summary>
    /// Contract for persisting and retrieving rate limit counters.
    /// Supports in-memory or distributed implementations.
    /// </summary>
    public interface IRateLimitStore
    {
        /// <summary>
        /// Attempts to increment the request counter for the given key.
        /// </summary>
        /// <param name="key">Unique rate limit key.</param>
        /// <param name="limit">Maximum allowed requests.</param>
        /// <param name="window">Time window.</param>
        /// <returns>Result of the rate limit check.</returns>
        Task<RateLimitResult> IncrementAsync(string key, int limit, TimeSpan window);
    }
}
