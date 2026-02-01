🔐 **Hrithik.Security.RateLimiting**

Enterprise-grade API Rate Limiting for ASP.NET Core

Hrithik.Security.RateLimiting is a lightweight, extensible rate-limiting library designed for
banking, fintech, and security-sensitive APIs.
It provides API key–based throttling, per-endpoint limits, and pluggable storage
with clean middleware integration.

This package is part of the Hrithik.Security ecosystem and is designed to work seamlessly
with:

Hrithik.Security.ApiKeyManagement

Hrithik.Security.RequestSigning

Hrithik.Security.ReplayProtection

✨ Features

🚦 API key–based rate limiting

🎯 Per-endpoint and global rate limit rules

🪟 Sliding-window rate limiting algorithm

🧩 Pluggable storage (In-Memory, Redis)

⚙️ ASP.NET Core middleware integration

🔐 Designed for banking & fintech APIs

📦 NuGet-friendly, minimal dependencies

📦 Installation
dotnet add package Hrithik.Security.RateLimiting


For distributed environments, install Redis support:

dotnet add package Hrithik.Security.RateLimiting.Redis

🚀 Quick Start
1️⃣ Register Services
builder.Services.AddHrithikRateLimiting(options =>
{
    // Global rule
    options.GlobalRules.Add(new RateLimitRule
    {
        Requests = 100,
        Per = TimeSpan.FromMinutes(1)
    });

    // Per-endpoint rule
    options.EndpointRules["POST:/payments"] = new RateLimitRule
    {
        Requests = 10,
        Per = TimeSpan.FromSeconds(30)
    };
});

2️⃣ Add Middleware
app.UseHrithikRateLimiting();

🔑 Rate Limit Identity Resolution

Requests are rate-limited using the following priority:

API Key ID (from Hrithik.Security.ApiKeyManagement)

Signed Client ID (from Hrithik.Security.RequestSigning)

IP Address (fallback)

This ensures accurate and fair throttling in multi-tenant systems.

🧠 How It Works
Incoming Request
      ↓
RateLimitingMiddleware
      ↓
SlidingWindowLimiter
      ↓
IRateLimitStore
   ├── InMemoryRateLimitStore
   └── RedisRateLimitStore


The middleware is storage-agnostic and can scale from single-instance APIs to
distributed, load-balanced systems.

📊 Rate Limit Response

When a limit is exceeded, the API returns:

HTTP 429 – Too Many Requests

Headers:

Retry-After: <seconds>


Response body:

{
  "error": "rate_limit_exceeded",
  "retryAfter": 42
}



🏦 Designed for Fintech & Banking

This package follows security-first design principles:

No hard dependency on ASP.NET Core JSON helpers

Minimal surface area

Deterministic behavior

Predictable throttling semantics

Ideal for:

Payment APIs

Partner integrations

FIX / gateway services

Internal microservices

📌 Target Framework

.NET 6+

.NET 7

.NET 8

(Compatible with netstandard2.0-based consumers)

📄 License

MIT License

👤 Author

Hrithik Kalra

📧 Email: hrithikkalra11@gmail.com

☕ Buy me a coffee: https://www.buymeacoffee.com/alkylhalid9
or
consider supporting its development: 👉 https://github.com/sponsors/hrithikalra

🔗 Related Packages

Hrithik.Security.ApiKeyManagement

Hrithik.Security.RequestSigning

Hrithik.Security.ReplayProtection

Hrithik.Security.Idempotency (coming soon)
