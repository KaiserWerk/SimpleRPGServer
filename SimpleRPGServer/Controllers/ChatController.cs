using Microsoft.AspNetCore.Mvc;
using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Ingame;
using SimpleRPGServer.Seeds;
using SimpleRPGServer.Service;
using SimpleRPGServer.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRPGServer.Controllers
{
    [ApiController]
    [Route("api/gamedata/chat")]
    public class ChatController : ControllerBase
    {
        private readonly GameDbContext _context;
        private readonly IChatService _chat;

        public ChatController(GameDbContext context, IChatService chatService)
        {
            this._context = context;
            this._chat = chatService;
        }

        [HttpGet]
        [Route("message/list")]
        public async Task<ActionResult<List<ChatMessage>>> GetAllMessages()
        {
            var login = HttpUtil.GetLoginFromHeader(this.Request, this._context);
            if (login == null || login.PlayerId == 0)
                return BadRequest();

            Player player = this._context.Players.SingleOrDefault(p => p.Id == login.PlayerId);
            if (player == null)
                return BadRequest();

            return this._chat.GetChatMessages(player).ToList();
        }

        [HttpPost]
        [Route("fieldmessage")]
        public async Task<IActionResult> AddFieldMessage(NewChatMessage message)
        {
            var login = HttpUtil.GetLoginFromHeader(this.Request, this._context);
            if (login == null || login.PlayerId == 0)
                return BadRequest();

            Player player = this._context.Players.SingleOrDefault(p => p.Id == login.PlayerId);
            if (player == null)
                return BadRequest();

            this._chat.AddFieldMessage(player, player.X, player.Y, message.Message);
            await this._context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("shoutmessage")]
        public async Task<IActionResult> AddShoutMessage(NewChatMessage message)
        {
            var login = HttpUtil.GetLoginFromHeader(this.Request, this._context);
            if (login == null || login.PlayerId == 0)
                return BadRequest();

            Player player = this._context.Players.SingleOrDefault(p => p.Id == login.PlayerId);
            if (player == null)
                return BadRequest();

            this._chat.AddShoutMessage(player, message.Message);
            await this._context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("clanmessage")]
        public async Task<IActionResult> AddClanMessage(NewChatMessage message)
        {
            var login = HttpUtil.GetLoginFromHeader(this.Request, this._context);
            if (login == null || login.PlayerId == 0)
                return BadRequest();

            Player player = this._context.Players.SingleOrDefault(p => p.Id == login.PlayerId);
            if (player == null)
                return BadRequest();

            this._chat.AddClanMessage(player, player.Clan, message.Message);
            await this._context.SaveChangesAsync();

            return Ok();
        }
    }
}
