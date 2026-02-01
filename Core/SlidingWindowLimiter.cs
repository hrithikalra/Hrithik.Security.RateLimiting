using Hrithik.Security.RateLimiting.Abstractions;
using Hrithik.Security.RateLimiting.Configuration;

namespace Hrithik.Security.RateLimiting.Core
{
    /// <summary>
    /// Implements a sliding window rate limiting algorithm.
    /// </summary>
    public sealed class SlidingWindowLimiter
    {
        private readonly IRateLimitStore _store;

        /// <summary>
        /// Initializes a new instance of <see cref="SlidingWindowLimiter"/>.
        /// </summary>
        public SlidingWindowLimiter(IRateLimitStore store)
        {
            _store = store;
        }

        /// <summary>
        /// Evaluates whether a request is allowed.
        /// </summary>
        public Task<RateLimitResult> CheckAsync(
            string key,
            RateLimitRule rule)
        {
            return _store.IncrementAsync(key, rule.Requests, rule.Per);
        }
    }
}
