using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleRPGServer.Persistence.Models;
using SimpleRPGServer.Persistence.Models.Auth;
using SimpleRPGServer.Persistence.Models.Ingame;
using SimpleRPGServer.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRPGServer.Controllers;

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
        await this._context.Players.AddAsync(player);

        await this._context.SaveChangesAsync();

        AuthAction authAction = new AuthAction(player.Id, "confirm_registration", DateTime.Now.AddDays(3));
        await this._context.AuthActions.AddAsync(authAction);

        // save changes
        await this._context.SaveChangesAsync();

        // send out confirmation email
        await this._mailer.SendRegistrationConfirmationMail(reg, authAction.Code.ToString());

        return Created("RegisterNewPlayer", new { });
    }

    [HttpGet("confirm/{code}")]
    public async Task<ActionResult> ConfirmRegistration(string code)
    {
        AuthAction action = await this._context.AuthActions.SingleOrDefaultAsync(aa => aa.Code == code);
        if (action == null || action.PlayerId == 0)
            return NotFound();

        if (action.Action != "confirm_registration")
            return BadRequest();

        action.ValidUntil = DateTime.Now.AddHours(-1);

        Player player = await this._context.Players.SingleOrDefaultAsync(p => p.Id == action.PlayerId);
        player.Locked = false;

        await this._context.SaveChangesAsync();

        return Ok("success");
    }
}
