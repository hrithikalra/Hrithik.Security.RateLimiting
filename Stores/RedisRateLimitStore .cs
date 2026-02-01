//using Hrithik.Security.RateLimiting.Abstractions;
//using Hrithik.Security.RateLimiting.Core;
//using StackExchange.Redis;

//namespace Hrithik.Security.RateLimiting.Stores
//{
//    /// <summary>
//    /// Redis-based rate limit store for distributed environments.
//    /// </summary>
//    public sealed class RedisRateLimitStore : IRateLimitStore
//    {
//        private readonly IDatabase _database;

//        /// <summary>
//        /// Initializes a new instance of <see cref="RedisRateLimitStore"/>.
//        /// </summary>
//        public RedisRateLimitStore(IConnectionMultiplexer multiplexer)
//        {
//            _database = multiplexer.GetDatabase();
//        }

//        /// <inheritdoc />
//        public async Task<RateLimitResult> IncrementAsync(
//            string key,
//            int limit,
//            TimeSpan window)
//        {
//            var redisKey = $"ratelimit:{key}";
//            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

//            var count = await _database.StringIncrementAsync(redisKey);

//            if (count == 1)
//            {
//                await _database.KeyExpireAsync(redisKey, window);
//            }

//            var ttl = await _database.KeyTimeToLiveAsync(redisKey);

//            return new RateLimitResult
//            {
//                IsAllowed = count <= limit,
//                Remaining = Math.Max(0, limit - (int)count),
//                RetryAfter = ttl ?? TimeSpan.Zero
//            };
//        }
//    }
//}
