using Microsoft.EntityFrameworkCore;
using SimpleRPGServer.Extensions;
using SimpleRPGServer.Persistence.Models;
using SimpleRPGServer.Persistence.Models.Ingame;
using SimpleRPGServer.Util;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace SimpleRPGServer.Service;

public class NpcService : INpcService
{
    private Timer timer;
    private GameDbContext context;
    private IPlayerService playerService;

    public NpcService(GameDbContext context, IPlayerService playerService)
    {
        this.context = context;
        this.playerService = playerService;

        this.timer = new Timer(60000);
        this.timer.Elapsed += async (sender, e) => await this.CheckNpcs();
        this.timer.Start();
    }

    private async Task CheckNpcs()
    {
        try
        {
            int npcCount = await this.context.Npcs.CountAsync();
            int fieldCount = await this.context.MapFields.CountAsync();
            var loginCount = await this.context.PlayerLogins.CountAsync();
           
            int maxCount = fieldCount + loginCount * 2;
            if (npcCount >= maxCount)
                return;

            int numToCreate = 1 + loginCount / 3;
            for (int i = 0; i < numToCreate; i++)
            {
                BaseNpc baseNpc = this.context.BaseNpcs.Random();
                (int, int) coords = MapUtil.RandomCoordinatesFromRange(
                    (baseNpc.SpawnXStart, baseNpc.SpawnYStart),
                    (baseNpc.SpawnXEnd, baseNpc.SpawnYEnd)
                );
                Npc npc = new Npc(baseNpc, coords.Item1, coords.Item2);
                await this.context.Npcs.AddAsync(npc);
            }
            await this.context.SaveChangesAsync();
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
            await this.context.Npcs.AddAsync(npc);

            if (player.CurrentHealth < 0)
                player.CurrentHealth = 0;
            result.NpcSurvived = false;
            result.PlayerHealthLeft = player.CurrentHealth;
            result.ExperienceGained = npc.BaseNpc.GrantExperience;

        }
        else if (player.CurrentHealth < 0)
        {
            await this.playerService.KillPlayerFromNpcAsync(player);
            result.NpcSurvived = true;
            result.NpcHealthLeft = npc.CurrentHealth;
        }
        await this.context.SaveChangesAsync();

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
            await this.context.DroppedGold.AddAsync(droppedGold);
        }

        if (npc.BaseNpc.ItemTable != null)
        {
            var drops = npc.BaseNpc.ItemTable.GetDrops();
            if (!drops.Any())
                return;
            foreach (var drop in drops)
            {
                var item = new PlayerItem(null, drop.BaseItem, npc.X, npc.Y);
                await this.context.PlayerItems.AddAsync(item);
            }
        }
        await this.context.SaveChangesAsync();
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

        // TODO
    }

    ~NpcService()
    {
        this.timer.Stop();
        this.timer.Dispose();
    }
}
