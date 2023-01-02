using Microsoft.AspNetCore.Mvc;
using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Ingame;
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
            if (login == null || login.Player == null)
                return BadRequest();

            return this._chat.GetChatMessages(login.Player).ToList();
        }

        [HttpPost]
        [Route("fieldmessage/add")]
        public async Task<IActionResult> AddFieldMessage(string message)
        {
            var login = HttpUtil.GetLoginFromHeader(this.Request, this._context);
            if (login == null || login.Player == null)
                return BadRequest();

            this._chat.AddFieldMessage(login.Player, login.Player.X, login.Player.Y, message);
            await this._context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("shoutmessage")]
        public async Task<IActionResult> AddShoutMessage(string message)
        {
            var login = HttpUtil.GetLoginFromHeader(this.Request, this._context);
            if (login == null || login.Player == null)
                return BadRequest();

            this._chat.AddShoutMessage(login.Player, message);
            await this._context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("clanmessage")]
        public async Task<IActionResult> AddClanMessage(string message)
        {
            var login = HttpUtil.GetLoginFromHeader(this.Request, this._context);
            if (login == null || login.Player == null)
                return BadRequest();

            this._chat.AddClanMessage(login.Player, login.Player.Clan, message);
            await this._context.SaveChangesAsync();

            return Ok();
        }
    }
}
