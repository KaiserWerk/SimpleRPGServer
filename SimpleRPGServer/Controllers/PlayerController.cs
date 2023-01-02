using Microsoft.AspNetCore.Mvc;
using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Ingame;
using SimpleRPGServer.Util;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRPGServer.Controllers
{
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
            if (login == null || login.Player == null)
                return BadRequest();

            return login.Player.ToApiData();
        }
    }
}
