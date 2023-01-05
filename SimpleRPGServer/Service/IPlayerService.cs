using SimpleRPGServer.Models.Ingame;

namespace SimpleRPGServer.Service
{
    public interface IPlayerService
    {
        void KillPlayerFromEnv(Player player);
    }
}