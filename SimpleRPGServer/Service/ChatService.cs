using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Ingame;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleRPGServer.Service
{
    public class ChatService : IChatService
    {
        private readonly GameDbContext _context;
        public ChatService(GameDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<ChatMessage> GetChatMessages(Player player)
        {
            return this._context.ChatMessages
                .Where(cm =>
                    cm.MessageType == MessageType.FieldSay ||
                    cm.MessageType == MessageType.FieldSay ||
                    (player.Clan != null && cm.MessageType == MessageType.Clan && cm.Clan.Id == player.Clan.Id)
                );
        }

        public IEnumerable<ChatMessage> GetInfoMessages()
        {
            return this._context.ChatMessages
                .Where(cm => cm.MessageType == MessageType.FieldInfo || cm.MessageType == MessageType.GlobalInfo);
        }

        public void AddFieldInfoMessage(int x, int y, string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new Exception("missing or empty parameter");

            var msg = new ChatMessage()
            {
                Message = message,
                MessageType = MessageType.GlobalInfo,
                SentAt = DateTime.Now,
                X = x,
                Y = y,
            };

            this._context.ChatMessages.Add(msg);
            this._context.SaveChanges();
        }

        public void AddGlobalInfoMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new Exception("missing or empty parameter");

            var msg = new ChatMessage()
            {
                Message = message,
                MessageType = MessageType.FieldInfo,
                SentAt = DateTime.Now,
            };

            this._context.ChatMessages.Add(msg);
            this._context.SaveChanges();
        }

        public void AddFieldMessage(Player player, int x, int y, string message)
        {
            if (player == null || string.IsNullOrEmpty(message))
                throw new Exception("missing or empty parameter");

            var msg = new ChatMessage()
            {
                Message = message,
                MessageType = MessageType.FieldSay,
                Sender = player,
                SentAt = DateTime.Now,
                X = x,
                Y = y,
            };

            this._context.ChatMessages.Add(msg);
            this._context.SaveChanges();
        }

        public void AddShoutMessage(Player player, string message)
        {
            if (player == null || string.IsNullOrEmpty(message))
                throw new Exception("missing or empty parameter");

            var msg = new ChatMessage()
            {
                Message = message,
                MessageType = MessageType.Shout,
                Sender = player,
                SentAt = DateTime.Now,
            };

            this._context.ChatMessages.Add(msg);
            this._context.SaveChanges();
        }

        public void AddClanMessage(Player player, Clan clan, string message)
        {
            if (player == null || clan == null || string.IsNullOrEmpty(message))
                throw new Exception("missing or empty parameter");

            var msg = new ChatMessage()
            {
                Message = message,
                MessageType = MessageType.Clan,
                Sender = player,
                Clan = clan,
                SentAt = DateTime.Now,
            };

            this._context.ChatMessages.Add(msg);
            this._context.SaveChanges();
        }
    }
}
