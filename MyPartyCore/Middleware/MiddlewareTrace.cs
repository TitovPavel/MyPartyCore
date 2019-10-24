using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MyPartyCore.Middleware
{
    public class MiddlewareTrace
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _env;
        static readonly object _locker = new object();

        public MiddlewareTrace(RequestDelegate next, IHostingEnvironment env)
        {
            this._next = next;
            this._env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            string headersRequest = string.Empty;
            foreach (var header in context.Request.Headers)
            {
                headersRequest += $"{header.Key}:  {header.Value}{Environment.NewLine}";
            }

            string path = Path.Combine(_env.WebRootPath, "Log.txt");
            string logInfoRequest = $"Time: {DateTime.Now} {Environment.NewLine}QueryString: {context.Request.Path.ToString()} {Environment.NewLine}Headers: {headersRequest} {Environment.NewLine}";

            lock (_locker)
                using (StreamWriter fs = new StreamWriter(path, true))
                {

                    fs.Write(logInfoRequest);
                }

            await _next.Invoke(context);

            string headersResponse = string.Empty;
            foreach (var header in context.Response.Headers)
            {
                headersResponse += $"{header.Key}:  {header.Value}{Environment.NewLine}";
            }
            string logInfoResponse = $"Time: {DateTime.Now} {Environment.NewLine}Headers: {headersResponse} {Environment.NewLine}";

            lock (_locker)
                using (StreamWriter fs = new StreamWriter(path, true))
                {
                    fs.Write(logInfoResponse);
                }

        }
    }

    public static class MiddlewareTraceExtensions
    {
        public static IApplicationBuilder UseMiddlewareTrace(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MiddlewareTrace>();
        }
    }
}
