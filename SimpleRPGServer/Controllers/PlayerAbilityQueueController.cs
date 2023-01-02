using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Ingame;
using SimpleRPGServer.Util;
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
            if (login == null || login.Player == null)
                return BadRequest();

            return await this._context.PlayerAbilityQueues.SingleOrDefaultAsync(paq => paq.Player.Id == login.Player.Id);
        }
    }
}
