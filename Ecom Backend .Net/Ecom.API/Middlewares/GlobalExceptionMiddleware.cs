
using Microsoft.Extensions.Caching.Memory;

namespace Ecom.API.Middlewares
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);

        public GlobalExceptionMiddleware(IHostEnvironment hostEnvironment, IMemoryCache memoryCache)
        {
            _hostEnvironment = hostEnvironment;
            _memoryCache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                ApplySecurity(context);

                if (!IsRequestAllowed(context))
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 429; // Too Many Requests
                    var rateLimitResponse = new
                    {
                        StatusCode = 429,
                        Message = "Too many requests. Please try again later."
                    };
                    await context.Response.WriteAsJsonAsync(rateLimitResponse);
                    return;
                }
                await next(context);
            }
            catch (Exception ex)
            {
           
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                var response = new
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    Details = _hostEnvironment.IsDevelopment() ? ex.StackTrace?.ToString() : null
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }

        private bool IsRequestAllowed(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            var cacheKey = $"Rate:{ip}";
            var dateNow = DateTime.Now;

            var (timestamp, count) = _memoryCache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _rateLimitWindow;
                return (timestamp: dateNow, count: 0);
            });

            if (dateNow - timestamp < _rateLimitWindow)
            {
                if (count >= 5)
                {
                    return false;
                }

                _memoryCache.Set(cacheKey, (timestamp, count + 1), _rateLimitWindow);
            }

            return true;
        }

        private void ApplySecurity(HttpContext context)
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            context.Response.Headers["X-XSS-Protection"] = "1;mode=block";
            context.Response.Headers["X-Frame-Options"] = "DENY";

        }


    }


}
