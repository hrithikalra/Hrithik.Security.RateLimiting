using Hrithik.Security.RateLimiting.Abstractions;
using Hrithik.Security.RateLimiting.Configuration;
using Hrithik.Security.RateLimiting.Core;
using Hrithik.Security.RateLimiting.Stores;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// DI extensions for rate limiting.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Registers rate limiting services.
        /// </summary>
        public static IServiceCollection AddHrithikRateLimiting(
            this IServiceCollection services,
            Action<RateLimitOptions> configure)
        {
            services.Configure(configure);

            services.AddSingleton<IRateLimitStore, InMemoryRateLimitStore>();
            services.AddSingleton<SlidingWindowLimiter>();

            return services;
        }
    }
}
