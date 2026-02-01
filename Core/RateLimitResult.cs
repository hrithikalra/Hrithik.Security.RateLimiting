namespace Hrithik.Security.RateLimiting.Core
{
    /// <summary>
    /// Represents the result of a rate limit evaluation.
    /// </summary>
    public sealed class RateLimitResult
    {
        /// <summary>
        /// Indicates whether the request is allowed.
        /// </summary>
        public bool IsAllowed { get; init; }

        /// <summary>
        /// Number of remaining requests.
        /// </summary>
        public int Remaining { get; init; }

        /// <summary>
        /// Time until the rate limit resets.
        /// </summary>
        public TimeSpan RetryAfter { get; init; }
    }
}
