using SimpleRPGServer.Models.Ingame;
using System;

namespace SimpleRPGServer.Models.Auth
{
    public class AuthAction
    {
        public long Id { get; set; }
        public Player Player { get; set; }
        public string Action { get; set; }
        public Guid Code { get; set; }
        public DateTime ValidUntil { get; set; }

        public AuthAction()
        { }
        public AuthAction(Player player, string action, DateTime validUntil)
        {
            this.Player = player;
            this.Action = action;
            this.ValidUntil = validUntil;
            this.Code = Guid.NewGuid();
        }
    }
}
