using SimpleRPGServer.Models.Ingame;
using System;

namespace SimpleRPGServer.Models.Auth
{
    public class AuthAction
    {
        public long Id { get; set; }
        public long PlayerId { get; set; }
        public string Action { get; set; }
        public string Code { get; set; }
        public DateTime ValidUntil { get; set; }

        public AuthAction()
        { }
        public AuthAction(long id, string action, DateTime validUntil)
        {
            this.PlayerId = id;
            this.Action = action;
            this.ValidUntil = validUntil;
            this.Code = Guid.NewGuid().ToString();
        }
    }
}
