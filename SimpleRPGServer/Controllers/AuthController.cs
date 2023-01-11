using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Auth;
using SimpleRPGServer.Models.Ingame;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SimpleRPGServer.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly GameDbContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(GameDbContext context, ILogger<AuthController> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<PlayerLogin>> Login(LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return Unauthorized();
            }
            if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return Unauthorized();
            }

            Player player = this._context.Players.SingleOrDefault(p => p.Email == loginRequest.Email);
            if (player == null)
            {
                this._logger.LogInformation("failed to find user for email");
                return Unauthorized();
            }

            if (player.Password != loginRequest.Password)
            {
                return Unauthorized();
            }
            if (player.Locked)
                return Unauthorized();

            string token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            PlayerLogin response = new PlayerLogin() 
            { 
                Player = player,
                Token = token, 
                ValidUntil = DateTime.UtcNow.AddHours(12),
            };

            await this._context.PlayerLogins.AddAsync(response);
            await this._context.SaveChangesAsync();

            return response;
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout(LogoutRequest logoutRequest)
        {
            if (logoutRequest == null)
            {
                return BadRequest();
            }

            PlayerLogin playerLogin = this._context.PlayerLogins.SingleOrDefault(pl => pl.Token == logoutRequest.Token);
            if (playerLogin == null)
            {
                return BadRequest();
            }

            if (playerLogin.ValidUntil < DateTime.UtcNow)
            {
                return NoContent();
            }

            playerLogin.ValidUntil = DateTime.Now.AddDays(-1);

            await this._context.SaveChangesAsync();

            return Ok();
        }
    }
}
