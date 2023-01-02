using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Ingame;
using SimpleRPGServer.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRPGServer.Controllers
{
    [ApiController]
    [Route("api/gamedata/item")]
    public class PlayerItemController : ControllerBase
    {
        private readonly GameDbContext _context;

        public PlayerItemController(GameDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<List<PlayerItem>>> GetPlayerItems()
        {
            var login = HttpUtil.GetLoginFromHeader(this.Request, this._context);
            if (login == null || login.Player == null)
                return BadRequest();

            return await this._context.PlayerItems.Where(pi => pi.Player.Id == login.Player.Id).ToListAsync();
        }
    }
}
