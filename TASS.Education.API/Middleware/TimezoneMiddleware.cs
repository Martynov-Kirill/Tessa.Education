using Microsoft.Extensions.Options;
using Tessa.Education.Entites.Models;

namespace Tessa.Education.API.Middleware
{
    public class TimezoneMiddleware
    {
        private RequestDelegate _next;
        private IOptions<TimezoneOptions> _options;
        public TimezoneMiddleware(RequestDelegate next, IOptions<TimezoneOptions> options)
        {
            _next = next;
            _options = options;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var user = context.User;
            var t = user.Claims.FirstOrDefault(c => c.Type == Constants.UTC_TIMEZONE);
            var timezoneTerm = t == null ? 0 : Convert.ToInt32(t.Value);
            _options.Value.TimezoneTerm = timezoneTerm;
            await _next(context);
        }
    }
}
