using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Ingame;
using SimpleRPGServer.Persistence.Models;
using SimpleRPGServer.Persistence.Models.Ingame;
using SimpleRPGServer.Service;
using SimpleRPGServer.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRPGServer.Controllers;

[ApiController]
[Route("api/gamedata/npc")]
public class NpcController : ControllerBase
{
    private GameDbContext _context;
    private INpcService _npcService;
    public NpcController(GameDbContext context, INpcService npcSevice)
    {
        this._context = context;
        this._npcService = npcSevice;
    }

    [HttpGet]
    [Route("field")]
    public async Task<ActionResult<List<Npc>>> GetNpcsForField()
    {
        var login = HttpUtil.GetLoginFromHeader(this.Request, this._context);
        if (login == null || login.PlayerId == 0)
            return BadRequest();

        Player player = this._context.Players.SingleOrDefault(p => p.Id == login.PlayerId);
        if (player == null)
            return BadRequest();

        return await this._context.Npcs.Where(n => n.X ==player.X && n.Y == player.Y).ToListAsync();
    }

    [HttpGet]
    [Route("attack/{id}")]
    public async Task<ActionResult<FightResult>> AttackNpc(ulong id)
    {
        var login = HttpUtil.GetLoginFromHeader(this.Request, this._context);
        if (login == null || login.PlayerId == 0)
            return BadRequest();

        Player player = this._context.Players.SingleOrDefault(p => p.Id == login.PlayerId);
        if (player == null)
            return BadRequest();

        Npc npc = await this._context.Npcs.SingleOrDefaultAsync(n => n.Id == id);
        if (npc == null)
            return BadRequest();

        if (npc.X != player.X || npc.Y != player.Y)
            return BadRequest();

        return await this._npcService.AttackNpc(npc, player);
    }
}
