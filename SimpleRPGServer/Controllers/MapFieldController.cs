using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleRPGServer.Persistence.Models;
using SimpleRPGServer.Persistence.Models.Ingame;
using SimpleRPGServer.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleRPGServer.Controllers;

[Route("api/gamedata/map_field")]
[ApiController]
public class MapFieldController : ControllerBase
{
    private readonly GameDbContext _context;

    public MapFieldController(GameDbContext context)
    {
        this._context = context;
    }

    [HttpGet]
    [Route("{x}/{y}")]
    public async Task<ActionResult<IEnumerable<MapField>>> GetMapFields(int x, int y)
    {
        var login = HttpUtil.GetLoginFromHeader(this.Request, this._context);
        if (login == null || login.PlayerId == 0)
            return BadRequest();

        var l = new List<MapField>();

        for (int i = x - 2; i < x + 2; i++)
        {
            for (int k = y - 2; k < y + 2; k++)
            {
                var field = await this._context.MapFields.SingleOrDefaultAsync(mf => mf.X == i && mf.Y == k);
                if (field == null)
                    field = MapField.BorderField(i, k);
                l.Add(field);
            }
        }

        return l;
    }


}
