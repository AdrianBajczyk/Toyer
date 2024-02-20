using Toyer.API.DevTools;

namespace Toyer.API.DevTools
{
    public class DelayMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TimeSpan _delay;

        public DelayMiddleware(RequestDelegate next, TimeSpan delay)
        {
            _next = next;
            _delay = delay;
        }

        public async Task Invoke(HttpContext context)
        {
            await Task.Delay(_delay);
            await _next(context);
        }
    }
}

public static class DelayMiddlewareExtensions
{
    public static IApplicationBuilder UseDelay(this IApplicationBuilder builder, TimeSpan delay)
    {
        return builder.UseMiddleware<DelayMiddleware>(delay);
    }
}