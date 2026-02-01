using Hrithik.Security.RateLimiting.Abstractions;
using Hrithik.Security.RateLimiting.Core;
using System.Collections.Concurrent;

namespace Hrithik.Security.RateLimiting.Stores
{
    /// <summary>
    /// In-memory rate limit store.
    /// Intended for single-instance deployments.
    /// </summary>
    public sealed class InMemoryRateLimitStore : IRateLimitStore
    {
        private sealed class Counter
        {
            public int Count;
            public DateTime WindowStart;
        }

        private readonly ConcurrentDictionary<string, Counter> _counters = new();

        /// <inheritdoc />
        public Task<RateLimitResult> IncrementAsync(string key, int limit, TimeSpan window)
        {
            var now = DateTime.UtcNow;

            var counter = _counters.GetOrAdd(key, _ =>
                new Counter { WindowStart = now });

            lock (counter)
            {
                if (now - counter.WindowStart >= window)
                {
                    counter.WindowStart = now;
                    counter.Count = 0;
                }

                counter.Count++;

                bool allowed = counter.Count <= limit;

                return Task.FromResult(new RateLimitResult
                {
                    IsAllowed = allowed,
                    Remaining = Math.Max(0, limit - counter.Count),
                    RetryAfter = allowed
                        ? TimeSpan.Zero
                        : window - (now - counter.WindowStart)
                });
            }
        }
    }
}
