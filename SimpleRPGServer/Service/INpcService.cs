using SimpleRPGServer.Models.Ingame;

namespace SimpleRPGServer.Service
{
    public interface INpcService
    {
        void AttackNpc(Npc npc, Player player);
        void HitNpc(Npc npc, Player player);
    }
}