using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Ingame;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleRPGServer.Controllers
{
    [ApiController]
    [Route("api/gamedata/base_item")]
    public class BaseItemController : ControllerBase
    {
        private readonly GameDbContext _context;

        public BaseItemController(GameDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<List<BaseItem>>> GetBaseItems()
        {
            return await this._context.BaseItems.ToListAsync();
        }
    }
}
