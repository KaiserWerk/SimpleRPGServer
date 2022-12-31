using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using SimpleRPGServer.Models;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace SimpleRPGServer.Middleware
{
    public class AuthTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthTokenMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Path.StartsWithSegments("/api/gamedata"))
            {
                await _next.Invoke(httpContext);
                return;
            }
            var dbContext = httpContext.RequestServices.GetService<GameDbContext>();
            Console.WriteLine($"Request for {httpContext.Request.Path} received ({httpContext.Request.ContentLength ?? 0} bytes)");
            bool hasAuthHeader = httpContext.Request.Headers.TryGetValue("X-Api-Token", out var token);
            if (hasAuthHeader && token.Any())
            {
                var playerLogin = dbContext.PlayerLogins.SingleOrDefault(pl => pl.Token == token[0]);
                if (playerLogin != null)
                {
                    if (playerLogin.IsValid())
                    {
                        // Call the next middleware delegate in the pipeline 
                        await _next.Invoke(httpContext);
                    }
                }
            }
        }
    }
}
