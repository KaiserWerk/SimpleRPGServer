using SimpleRPGServer.Models.Ingame;
using System.Threading.Tasks;

namespace SimpleRPGServer.Service
{
    public interface INpcService
    {
        Task<FightResult> AttackNpc(Npc npc, Player player);
        void HitNpc(Npc npc, Player player);
    }
}