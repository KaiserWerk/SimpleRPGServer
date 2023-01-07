using Microsoft.EntityFrameworkCore;
using SimpleRPGServer.Extensions;
using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Ingame;
using SimpleRPGServer.Util;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace SimpleRPGServer.Service
{
    public class NpcService : INpcService
    {
        private Timer _timer;
        private GameDbContext _context;
        private IPlayerService _playerService;

        public NpcService(GameDbContext context, IPlayerService playerService)
        {
            this._context = context;
            this._playerService = playerService;

            this._timer = new Timer(60000);
            this._timer.Elapsed += async (sender, e) => await this.CheckNpcs();
            this._timer.Start();
        }

        private async Task CheckNpcs()
        {
            try
            {
                int npcCount = await this._context.Npcs.CountAsync();
                int fieldCount = await this._context.MapFields.CountAsync();
                int loginCount = await this._context.PlayerLogins.Where(pl => !string.IsNullOrEmpty(pl.Token) && pl.ValidUntil > DateTime.UtcNow.AddMinutes(5)).CountAsync();
                int maxCount = fieldCount + loginCount * 2;
                if (npcCount >= maxCount)
                    return;

                int numToCreate = 1 + loginCount / 3;
                for (int i = 0; i < numToCreate; i++)
                {
                    BaseNpc baseNpc = this._context.BaseNpcs.Random();
                    (int, int) coords = MapUtil.RandomCoordinatesFromRange(
                        (baseNpc.SpawnXStart, baseNpc.SpawnYStart),
                        (baseNpc.SpawnXEnd, baseNpc.SpawnYEnd)
                    );
                    Npc npc = new Npc(baseNpc, coords.Item1, coords.Item2);
                    await this._context.Npcs.AddAsync(npc);
                }
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed to spawn NPCs: {ex.Message}");
            }
        }

        public async Task<FightResult> AttackNpc(Npc npc, Player player)
        {
            if (npc == null || npc.BaseNpc == null || player == null)
            {
                Console.WriteLine($"AttackNpc: some parameters are null");
                return null;
            }

            while (npc.CurrentHealth >= 0 && player.CurrentHealth >= 0)
            {
                this.HitNpc(npc, player);
            }

            FightResult result = new FightResult();

            if (npc.CurrentHealth < 0)
            {
                await this.PlaceNpcItemsAndGold(npc);
                await this._context.Npcs.AddAsync(npc);

                if (player.CurrentHealth < 0)
                    player.CurrentHealth = 0;
                result.NpcSurvived = false;
                result.PlayerHealthLeft = player.CurrentHealth;
                result.ExperienceGained = npc.BaseNpc.GrantExperience;

            } 
            else if (player.CurrentHealth < 0)
            {
                this._playerService.KillPlayerFromEnv(player);
                result.NpcSurvived = true;
                result.NpcHealthLeft = npc.CurrentHealth;
            }
            await this._context.SaveChangesAsync();

            return result;
        }

        private async Task PlaceNpcItemsAndGold(Npc npc)
        {
            if (npc.BaseNpc.GoldDrop > 0)
            { 
                var droppedGold = new DroppedGold()
                {
                    Amount = npc.BaseNpc.GoldDrop,
                    X = npc.X,
                    Y = npc.Y,
                };
                await this._context.DroppedGold.AddAsync(droppedGold);
            }

            if (npc.BaseNpc.DroppedItem != null)
            {
                if (MathUtil.HappensByChance(npc.BaseNpc.DropChance))
                {
                    var item = new PlayerItem(null, npc.BaseNpc.DroppedItem, npc.X, npc.Y);
                    await this._context.PlayerItems.AddAsync(item);
                }
            }
            await this._context.SaveChangesAsync();
        }

        public void HitNpc(Npc npc, Player player)
        {
            int playerAttackStrength = player.GetAttackStrength();
            int playerDefenseStrength = player.GetDefenseStrength();

            int npcAttackStrength = npc.BaseNpc.AttackStrength - playerDefenseStrength;
            if (npcAttackStrength < 1)
                npcAttackStrength = 1;

            npc.CurrentHealth -= playerAttackStrength;
            player.CurrentHealth -= npcAttackStrength;
        }

        ~NpcService()
        {
            this._timer.Stop();
            this._timer.Dispose();
        }
    }
}
