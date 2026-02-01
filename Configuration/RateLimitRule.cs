namespace Hrithik.Security.RateLimiting.Configuration
{
    /// <summary>
    /// Defines a single rate limit rule.
    /// </summary>
    public sealed class RateLimitRule
    {
        /// <summary>
        /// Maximum number of allowed requests.
        /// </summary>
        public int Requests { get; set; }

        /// <summary>
        /// Time window for the rate limit.
        /// </summary>
        public TimeSpan Per { get; set; }
    }
}
