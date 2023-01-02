using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Ingame;

namespace SimpleRPGServer.Controllers
{
    [Route("api/gamedata/base_ability")]
    [ApiController]
    public class BaseAbilityController : ControllerBase
    {
        private readonly GameDbContext _context;

        public BaseAbilityController(GameDbContext context)
        {
            _context = context;
            //context.Database.EnsureCreated();
        }

        // GET: api/BaseAbility
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<BaseAbility>>> GetBaseAbilities()
        {
            return await _context.BaseAbilities.ToListAsync();
        }
    }
}
