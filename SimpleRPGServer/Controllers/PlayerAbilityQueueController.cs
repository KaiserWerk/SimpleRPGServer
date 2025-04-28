using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleRPGServer.Persistence.Models;
using SimpleRPGServer.Persistence.Models.Ingame;
using SimpleRPGServer.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRPGServer.Controllers;

[ApiController]
[Route("api/gamedata/ability_queue")]
public class PlayerAbilityQueueController : ControllerBase
{
    private readonly GameDbContext _context;

    public PlayerAbilityQueueController(GameDbContext context)
    {
        this._context = context;
    }

    [HttpGet]
    [Route("get")]
    public async Task<ActionResult<List<PlayerAbility>>> GetAbilityQueue()
    {
        var login = HttpUtil.GetLoginFromHeader(this.Request, this._context);
        if (login == null || login.PlayerId == 0)
            return BadRequest();

        Player player = await this._context.Players.SingleOrDefaultAsync(p => p.Id == login.PlayerId);
        if (player == null)
            return BadRequest();

        return player.Abilities.Where(e => e.Queued == true).ToList();
    }
}
