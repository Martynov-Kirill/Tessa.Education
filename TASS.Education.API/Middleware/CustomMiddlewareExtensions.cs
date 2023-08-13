using Tessa.Education.BLL.Models;

namespace Tessa.Education.API.Middleware
{
    /// <summary>
    /// middle ware class
    /// </summary>
    public static class CustomMiddlewareExtensions
    {
        /// <summary>
        /// custom middleware Invoke methods
        /// </summary>
        /// <param name="app"></param>
        /// <param name="settings"></param>
        public static void ConfigureCustomMiddleware(this IApplicationBuilder app, AppSettings? settings = default)
        {
            app.UseMiddleware<ExceptionMiddleware>(settings);
            app.UseMiddleware<TimezoneMiddleware>();
        }
    }
}
