using SimpleRPGServer.Persistence.Models.Ingame;
using System.Threading.Tasks;

namespace SimpleRPGServer.Service;

public interface IPlayerService
{
    Task KillPlayerFromNpcAsync(Player player);
    Task EnqueueAbility(Player player, PlayerAbility playerAbility);
    Task SkillUpAbility(Player player, PlayerAbility playerAbility);
}