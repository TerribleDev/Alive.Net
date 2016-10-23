using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Alive.Net
{
    public class Alive
    {
        private RequestDelegate _next;

        internal AliveOptions Options { get; set; }

        public Alive(RequestDelegate next, AliveOptions options)
        {
            Options = options ?? new AliveOptions();
            if(next == null)
            {
                throw new ArgumentNullException(nameof(next), "RequestDelegate not passed in");
            }
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if(context.Request.Path.Equals(this.Options.LivecheckPath, StringComparison.CurrentCultureIgnoreCase))
            {
                var response = CalculateResponse(this.Options);
                context.Response.StatusCode = (int)response.StatusCode;
                if(!string.IsNullOrWhiteSpace(response.BodyText))
                {
                    await context.Response.WriteAsync(response.BodyText);
                }
            }
            else
            {
                _next?.Invoke(context);
            }
        }

        public static AliveResponse CalculateResponse(AliveOptions options)
        {
            if(options == null)
            {
                throw new ArgumentNullException("options");
            }
            var response = new AliveResponse();
            if(!string.IsNullOrWhiteSpace(options.BodyText))
            {
                response.BodyText = options.BodyText;
            }
            response.StatusCode = options.StatusCode;
            options.OnLivecheckResponse?.Invoke(response);
            return response;
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
    }
}