﻿using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using SimpleRPGServer.Persistence.Models;
using SimpleRPGServer.Persistence.Models.Ingame;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRPGServer.Service;

public class PlayerService : IPlayerService
{
    const decimal GOLD_DROP_FACTOR = 0.7m;
    const int ITEM_DROP_CHANCE_FACTOR = 15;
    private GameDbContext _context;
    private System.Timers.Timer _timer;

    //public PlayerService(GameDbContext context)
    //{
    //    this._context = context;
    //    this._timer = new System.Timers.Timer(60000);
    //    this._timer.Elapsed += (sender, e) => this.CheckAbilityQueues();
    //    this._timer.Start();
    //}

    //private void CheckAbilityQueues()
    //{
    //    var players = this._context.Players.ToList();
    //    if (players == null)
    //        throw new Exception("players is null");

    //    foreach (Player player in players)
    //    {
    //        PlayerAbilityQueue queue = player.AbilityQueue;
    //        if (queue == null)
    //            continue;
    //        if (queue.StartedAt > DateTime.Now)
    //        {
    //            if (queue.Player == null)
    //            {
    //                Console.WriteLine("queue's Player property was null");
    //                continue;
    //            }
    //            if (queue.BaseAbility == null)
    //            {
    //                Console.WriteLine("queue's BaseAbility property was null");
    //                continue;
    //            }

    //            var ability = this._context.PlayerAbilities
    //                .FirstOrDefault(pa => pa.Player.Id == queue.Player.Id && pa.BaseAbility.Id == queue.BaseAbility.Id);
    //            if (ability == null)
    //            {
    //                Console.WriteLine("player ability was not found");
    //                continue;
    //            }

    //            if (ability.CurrentLevel + 1 <= queue.BaseAbility.MaxLevel)
    //            {
    //                ability.CurrentLevel++;
    //                this._context.PlayerAbilityQueues.Remove(queue);
    //            }
    //        }
    //    }

    //    this._context.SaveChanges();
    //}

    public PlayerService(GameDbContext context)
    {
        this._context = context;
        this._timer = new System.Timers.Timer(60000);
        this._timer.Elapsed += async (sender, e) => await this.CheckAbilityTraining();
        this._timer.Start();
    }

    private async Task CheckAbilityTraining()
    {
        var players = await this._context.Players.ToListAsync();
        if (!players.Any())
            return;

        foreach (var player in players)
        {
            if (player.AbilityTraining == null)
                continue;
            if (player.AbilityTraining.StartedAt < DateTimeOffset.Now)
                continue;

            if (player.AbilityTraining.Ability.CurrentLevel + 1 <= player.AbilityTraining.Ability.BaseAbility.MaxLevel)
            {
                player.AbilityTraining.Ability.CurrentLevel++;
                player.AbilityTraining = null;
            }
        }

        await this._context.SaveChangesAsync();


    }

    public async Task KillPlayerFromNpcAsync(Player player)
    {

    }

    public async Task EnqueueAbility(Player player, PlayerAbility playerAbility)
    {

    }

    public async Task SkillUpAbility(Player player, PlayerAbility playerAbility)
    {

    }

    ~PlayerService()
    {
        this._timer.Stop();
        this._timer.Dispose();
    }
}
