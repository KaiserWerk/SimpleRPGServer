using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleRPGServer.Persistence.Models;
using SimpleRPGServer.Persistence.Models.Ingame;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleRPGServer.Controllers;

[Route("api/gamedata/base_ability")]
[ApiController]
public class BaseAbilityController : ControllerBase
{
    private readonly GameDbContext _context;

    public BaseAbilityController(GameDbContext context)
    {
        this._context = context;
    }

    [HttpGet]
    [Route("list")]
    public async Task<ActionResult<IEnumerable<BaseAbility>>> GetBaseAbilities()
    {
        return await this._context.BaseAbilities.ToListAsync();
    }
}
