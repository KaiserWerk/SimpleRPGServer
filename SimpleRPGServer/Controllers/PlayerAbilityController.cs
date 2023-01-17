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
    [Route("api/gamedata/ability")]
    public class PlayerAbilityController : ControllerBase
    {
        private readonly GameDbContext _context;
        public PlayerAbilityController(GameDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<List<PlayerAbilityData>>> GetPlayerAbilities()
        {
            var login = HttpUtil.GetLoginFromHeader(this.Request, this._context);
            if (login == null || login.PlayerId == 0)
                return BadRequest();

            Player player = this._context.Players.SingleOrDefault(p => p.Id == login.PlayerId);
            if (player == null)
                return BadRequest();

            return player.Abilities
                //.Where(pa => pa.Player.Id == login.Player.Id)
                .ToList()
                .ConvertAll(pa => pa.ToApiData());
        }
    }
}
