using Microsoft.AspNetCore.Mvc;
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
       
        public AuthController(GameDbContext context)
        {
            this._context = context;
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
                return Unauthorized();
            }

            if (player.Password != loginRequest.Password)
            {
                return Unauthorized();
            }

            string token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            PlayerLogin response = new PlayerLogin() 
            { 
                Player = player,
                Token = token, 
                ValidUntil = DateTime.UtcNow.AddHours(12),
            };

            this._context.PlayerLogins.Add(response);
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
