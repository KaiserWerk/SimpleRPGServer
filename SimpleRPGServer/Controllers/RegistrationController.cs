using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Auth;
using SimpleRPGServer.Models.Ingame;
using SimpleRPGServer.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRPGServer.Controllers
{
    [ApiController]
    [Route("api/registration")]
    public class RegistrationController : ControllerBase
    {
        private readonly GameDbContext _context;
        private readonly IEmailService _mailer;

        public RegistrationController(GameDbContext context, IEmailService emailService)
        {
            this._context = context;
            this._mailer = emailService;
        }

        [HttpPost]
        [Route("new")]
        public async Task<ActionResult> RegisterNewPlayer(RegistrationRequest reg)
        {
            if (reg == null)
            {
                return BadRequest();
            }
            if (string.IsNullOrEmpty(reg.Email) || string.IsNullOrEmpty(reg.DisplayName))
            {
                return BadRequest("missing a required field");
            }

            var existingPlayer = this._context.Players.SingleOrDefault(p => p.Email == reg.Email || p.DisplayName == reg.DisplayName);
            if (existingPlayer != null) 
            {
                return BadRequest("a player with this email or display name already exists");
            }

            // check if its a valid mail address
            // check if password is long enough

            Player player = new Player(reg.Email, reg.DisplayName, reg.Password);
            this._context.Players.Add(player);
            
            AuthAction authAction = new AuthAction(player, "registration", DateTime.Now.AddDays(3));
            this._context.AuthActions.Add(authAction);

            // save changes
            await this._context.SaveChangesAsync();

            // send out confirmation email
            await this._mailer.SendRegistrationConfirmationMail(reg, authAction.Code.ToString());

            return Created("RegisterNewPlayer", new { });
        }

        [HttpGet("confirm/{code}")]
        public async Task<ActionResult> ConfirmRegistration(string code)
        {
            return Ok();
        }
    }
}
