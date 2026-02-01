namespace Hrithik.Security.RateLimiting.Configuration
{
    /// <summary>
    /// Configuration options for rate limiting.
    /// </summary>
    public sealed class RateLimitOptions
    {
        /// <summary>
        /// Global rate limit rules.
        /// </summary>
        public IList<RateLimitRule> GlobalRules { get; } = new List<RateLimitRule>();

        /// <summary>
        /// Endpoint-specific rate limit rules.
        /// Key format: METHOD:/path
        /// </summary>
        public IDictionary<string, RateLimitRule> EndpointRules { get; }
            = new Dictionary<string, RateLimitRule>(StringComparer.OrdinalIgnoreCase);
    }
}
