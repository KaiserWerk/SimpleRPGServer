using Microsoft.AspNetCore.Mvc;
using SimpleRPGServer.Persistence.Models;
using SimpleRPGServer.Persistence.Models.Ingame;
using SimpleRPGServer.Util;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRPGServer.Controllers;

[ApiController]
[Route("api/gamedata/player")]
public class PlayerController : ControllerBase
{
    private readonly GameDbContext _context;

    public PlayerController(GameDbContext context)
    {
        this._context = context;
    }

    [HttpGet]
    [Route("me")]
    public async Task<ActionResult<PlayerData>> Me()
    {
        var login = HttpUtil.GetLoginFromHeader(this.Request, this._context);
        if (login == null || login.PlayerId == 0)
            return BadRequest();

        Player player = this._context.Players.SingleOrDefault(p => p.Id == login.PlayerId);
        if (player == null)
            return BadRequest();

        return player.ToApiData();
    }
}
