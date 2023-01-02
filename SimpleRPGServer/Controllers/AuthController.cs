using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Auth;
using SimpleRPGServer.Models.Ingame;
using SimpleRPGServer.Service;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SimpleRPGServer.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly GameDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _mailer;

        public AuthController(GameDbContext context, IConfiguration configuration, IEmailService emailService)
        {
            this._context = context;
            this._configuration = configuration;
            this._mailer = emailService;
            //context.Database.EnsureCreated();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
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

            return Json(response);
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
