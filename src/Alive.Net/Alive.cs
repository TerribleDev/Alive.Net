using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Alive.Net
{
    public class Alive
    {
        private RequestDelegate _next;

        private AliveOptions Options { get; set; }

        public Alive(RequestDelegate next, AliveOptions options)
        {
            Options = options ?? new AliveOptions();
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next), "RequestDelegate not passed in");
            }
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Equals(this.Options.LivecheckPath, StringComparison.CurrentCultureIgnoreCase))
            {
                context.Response.StatusCode = (int)this.Options.ReturnStatusCode;
                if (!string.IsNullOrWhiteSpace(Options.BodyText))
                {
                    await context.Response.WriteAsync(Options.BodyText);
                }
            }
            else
            {
                await _next?.Invoke(context);
            }
            //context.Request.Path.
        }
    }

    public static class BuilderExtension
    {
        /// <summary>
        /// Automatic livecheck
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options">Setup your options</param>
        public static void UseAlive(this IApplicationBuilder app, Action<AliveOptions> options)
        {
            var userOptions = new AliveOptions();
            options?.Invoke(userOptions);
            app.UseMiddleware<Alive>(userOptions);
        }

        /// <summary>
        /// Automatic livecheck
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options">Setup your options</param>
        public static void UseHeartBeat(this IApplicationBuilder app, Action<AliveOptions> options)
        {
            var userOptions = new AliveOptions();
            options?.Invoke(userOptions);
            app.UseMiddleware<Alive>(userOptions);
        }
    }
}