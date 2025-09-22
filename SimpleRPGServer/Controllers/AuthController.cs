using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleRPGServer.Persistence.Models;
using SimpleRPGServer.Persistence.Models.Auth;
using SimpleRPGServer.Persistence.Models.Ingame;
using SimpleRPGServer.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRPGServer.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly GameDbContext context;
    private readonly ILogger<AuthController> logger;
    private readonly ITokenGenerator tokenGenerator;

    public AuthController(ILogger<AuthController> logger, GameDbContext context, ITokenGenerator tokenGen)
    {
        this.logger = logger;
        this.context = context;
        this.tokenGenerator = tokenGen;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginRequest)
    {
        if (loginRequest == null)
        {
            return new LoginResponse(TechnicalCode.MissingCredentials);
        }
        if (!loginRequest.Valid)
        {
            return new LoginResponse(TechnicalCode.MissingCredentials);
        }

        Player player = this.context.Players.SingleOrDefault(p => p.Email == loginRequest.Email);
        if (player == null)
        {
            this.logger.LogInformation("failed to find user for email");
            return new LoginResponse(TechnicalCode.IncorrectCredentials);
        }

        if (player.Password != loginRequest.Password) // TODO: hashing
        {
            return new LoginResponse(TechnicalCode.IncorrectCredentials);
        }
        if (player.Locked)
            return new LoginResponse(TechnicalCode.AccountLocked);

        LoginResponse response = new LoginResponse(TechnicalCode.Ok, this.tokenGenerator.GenerateToken(64), DateTime.UtcNow.AddHours(12), player.ToApiData())
        {
            PlayerId = player.Id,
        };

        await this.context.PlayerLogins.AddAsync(response);
        await this.context.SaveChangesAsync();

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

        LoginResponse playerLogin = this.context.PlayerLogins.SingleOrDefault(pl => pl.Token == logoutRequest.Token);
        if (playerLogin == null)
        {
            return BadRequest();
        }

        if (playerLogin.ValidUntil < DateTime.UtcNow)
        {
            return NoContent();
        }

        playerLogin.ValidUntil = DateTime.Now.AddDays(-1);

        await this.context.SaveChangesAsync();

        return Ok();
    }
}
