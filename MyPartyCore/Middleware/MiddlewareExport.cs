using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MyPartyCore.DB.BL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.Middleware
{
    public class MiddlewareExport
    {
        private readonly RequestDelegate _next;

        public MiddlewareExport(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context, IPartyService partyService)
        {
            if (context.Request.Path.Value == "/Participants")
            {

                List<String> participants = partyService.ListAll().Select(x => x.Name).ToList();
                String stringParticipants = String.Join(";", participants);

                context.Response.ContentType = "text/csv";
                context.Response.Headers.Add("Content-Disposition", "attachment;filename=Participants.csv");
                await context.Response.WriteAsync(stringParticipants);
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }

    public static class MiddlewareExportExtensions
    {
        public static IApplicationBuilder UseMiddlewareExport(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MiddlewareExport>();
        }
    }
}
