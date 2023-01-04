using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Ingame;

namespace SimpleRPGServer.Service
{
    public class PlayerService
    {
        const decimal GOLD_DROP_FACTOR = 0.7m;
        private GameDbContext _context;

        public PlayerService(GameDbContext context)
        {
            this._context = context;
        }

        public void KillPlayerFromEnv(Player player)
        {

        }
    }
}
