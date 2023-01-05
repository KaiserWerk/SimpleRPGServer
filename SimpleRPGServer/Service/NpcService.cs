using SimpleRPGServer.Extensions;
using SimpleRPGServer.Models;
using SimpleRPGServer.Models.Ingame;
using SimpleRPGServer.Util;
using System;
using System.Linq;
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
            this._timer.Elapsed += this.CheckNpcs;
            this._timer.Start();
        }

        private void CheckNpcs(object sender, ElapsedEventArgs e)
        {
            try
            {
                int npcCount = this._context.Npcs.Count();
                int fieldCount = this._context.MapFields.Count();
                int loginCount = this._context.PlayerLogins.Where(pl => !string.IsNullOrEmpty(pl.Token) && pl.ValidUntil > DateTime.UtcNow.AddMinutes(5)).Count();
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
                    this._context.Npcs.Add(npc);
                }
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed to spawn NPCs: {ex.Message}");
            }
        }

        public void AttackNpc(Npc npc, Player player)
        {
            if (npc == null || npc.BaseNpc == null || player == null)
            {
                Console.WriteLine($"AttackNpc: some parameters are null");
                return;
            }

            while (npc.CurrentHealth >= 0 && player.CurrentHealth >= 0)
            {
                this.HitNpc(npc, player);
            }

            if (npc.CurrentHealth < 0)
            {
                this.PlaceNpcItemsAndGold(npc);
                this._context.Npcs.Remove(npc);
            }

            if (player.CurrentHealth < 0)
            {
                this._playerService.KillPlayerFromEnv(player);
            }

            this._context.SaveChanges();
        }

        private void PlaceNpcItemsAndGold(Npc npc)
        {
            var droppedGold = new DroppedGold()
            {
                Amount = npc.BaseNpc.GoldDrop,
                X = npc.X,
                Y = npc.Y,
            };
            this._context.DroppedGold.Add(droppedGold);

            if (npc.BaseNpc.DroppedItem != null)
            {
                if (MathUtil.HappensByChance(npc.BaseNpc.DropChance))
                {
                    var item = new PlayerItem(null, npc.BaseNpc.DroppedItem, npc.X, npc.Y);
                    this._context.PlayerItems.Add(item);
                }
            }
            this._context.SaveChanges();
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
