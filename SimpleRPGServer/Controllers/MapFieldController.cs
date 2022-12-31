using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Ingame;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleRPGServer.Controllers
{
    [Route("api/gamedata/map_field")]
    [ApiController]
    public class MapFieldController : ControllerBase
    {
        private readonly GameDbContext _context;

        public MapFieldController(GameDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<MapField>>> GetMapFields()
        {
            return await _context.MapFields.ToListAsync();
        }

       
    }
}
