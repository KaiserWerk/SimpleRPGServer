using Microsoft.AspNetCore.Http;
using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Auth;
using System.Linq;

namespace SimpleRPGServer.Util
{
    public static class HttpUtil
    {
        public static PlayerLogin GetLoginFromHeader(HttpRequest request, GameDbContext context)
        {
            var headerExists = request.Headers.TryGetValue("X-Api-Token", out var values);
            if (!headerExists || !values.Any())
            {
                return null;
            }

            return context.PlayerLogins.SingleOrDefault(pl => pl.Token == values[0]);
        }
    }
}
