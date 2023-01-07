using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Ingame;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SimpleRPGServer.Service
{
    public class PlayerService : IPlayerService
    {
        const decimal GOLD_DROP_FACTOR = 0.7m;
        const int ITEM_DROP_CHANCE_FACTOR = 15;
        private GameDbContext _context;
        private System.Timers.Timer _timer;

        public PlayerService(GameDbContext context)
        {
            this._context = context;
            this._timer = new System.Timers.Timer(60000);
            this._timer.Elapsed += async (sender, e) => await this.CheckAbilityQueues();
        }

        private async Task CheckAbilityQueues()
        {
            var queues = await this._context.PlayerAbilityQueues.ToListAsync();
            if (queues == null)
                throw new Exception("player ability queues is null");

            foreach (PlayerAbilityQueue queue in queues)
            {
                if (queue.Player == null)
                {
                    Console.WriteLine("queue's Player property was null");
                    continue;
                }
                if (queue.StartedAt > DateTime.Now)
                {
                    if (queue.BaseAbility == null)
                    {
                        Console.WriteLine("queue's BaseAbility property was null");
                        continue;
                    }

                    var ability = await this._context.PlayerAbilities
                        .FirstOrDefaultAsync(pa => pa.Player.Id == queue.Player.Id && pa.BaseAbility.Id == queue.BaseAbility.Id);
                    if (ability == null)
                    {
                        Console.WriteLine("player ability was not found");
                        continue;
                    }

                    if (ability.CurrentLevel + 1 <= queue.BaseAbility.MaxLevel)
                    {
                        ability.CurrentLevel++;
                        this._context.PlayerAbilityQueues.Remove(queue);
                    }
                }
            }

            await this._context.SaveChangesAsync();
        }

        public async Task KillPlayerFromEnv(Player player)
        {

        }

        public async Task EnqueueAbility(Player player, PlayerAbility playerAbility)
        {

        }

        public async Task SkillUpAbility(Player player, PlayerAbility playerAbility)
        {

        }


    }
}
