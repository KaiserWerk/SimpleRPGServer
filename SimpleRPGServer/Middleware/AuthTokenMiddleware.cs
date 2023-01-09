using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SimpleRPGServer.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRPGServer.Middleware
{
    public class AuthTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HEADER_NAME = "X-Api-Token";

        public AuthTokenMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //if (!httpContext.Request.Path.StartsWithSegments("/api/gamedata"))
            //{
            //    await _next.Invoke(httpContext);
            //    return;
            //}

            var dbContext = httpContext.RequestServices.GetService<GameDbContext>();
            Console.WriteLine($"Request for {httpContext.Request.Path} received ({httpContext.Request.ContentLength ?? 0} bytes)");
            bool hasAuthHeader = httpContext.Request.Headers.TryGetValue(HEADER_NAME, out var token);
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

            //httpContext.Response.StatusCode = 401;
        }
    }
}
