using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Ingame;
using SimpleRPGServer.Util;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRPGServer.Controllers
{
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
        public async Task<ActionResult<PlayerAbilityQueue>> GetAbilityQueue()
        {
            var login = HttpUtil.GetLoginFromHeader(this.Request, this._context);
            if (login == null || login.PlayerId == 0)
                return BadRequest();

            Player player = this._context.Players.SingleOrDefault(p => p.Id == login.PlayerId);
            if (player == null)
                return BadRequest();

            return await this._context.PlayerAbilityQueues.SingleOrDefaultAsync(paq => paq.Player.Id == player.Id);
        }
    }
}
