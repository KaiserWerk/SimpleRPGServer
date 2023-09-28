using Microsoft.EntityFrameworkCore;
using SimpleRPGServer.Models.Auth;
using SimpleRPGServer.Models.Ingame;
using System;
using System.IO;

namespace SimpleRPGServer.Models
{
    public class GameDbContext : DbContext
    {
        private readonly string dbFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SimpleRPGServer");
        private readonly string dbFileName = "SimpleRPGServer.db";

        // Auth
        public DbSet<AuthAction> AuthActions { get; set; }
        public DbSet<PlayerLogin> PlayerLogins { get; set; }

        // Ingame
        public DbSet<BaseAbility> BaseAbilities { get; set; }
        public DbSet<BaseItem> BaseItems { get; set; }
        public DbSet<BaseNpc> BaseNpcs { get; set; }
        public DbSet<MapField> MapFields { get; set; }
        public DbSet<Npc> Npcs { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerAbility> PlayerAbilities { get; set; }
        public DbSet<PlayerAbilityQueue> PlayerAbilityQueues { get; set; }
        public DbSet<PlayerItem> PlayerItems { get; set; }
        public DbSet<DroppedGold> DroppedGold { get; set; }

        public DbSet<Clan> Clans { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!Directory.Exists(this.dbFilePath))
                Directory.CreateDirectory(this.dbFilePath);
            
            optionsBuilder.UseSqlite($"Data Source={Path.Combine(this.dbFilePath, this.dbFileName)}");
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<PlayerLogin>().HasOne(pl => pl.Player).WithMany(p => p.Logins);

            modelBuilder.Entity<Player>().HasIndex(p => p.Email).IsUnique(true);
            modelBuilder.Entity<Player>().HasIndex(p => p.DisplayName).IsUnique(true);

            modelBuilder.Entity<Player>().OwnsOne(p => p.AbilityQueue);
            modelBuilder.Entity<Player>().OwnsOne(p => p.EquippedAttackWeapon);
            modelBuilder.Entity<Player>().OwnsOne(p => p.EquippedDefenseWeapon);
            modelBuilder.Entity<Player>().OwnsMany(p => p.Abilities);
            modelBuilder.Entity<Player>().OwnsMany(p => p.Items);
            //modelBuilder.Entity<Player>().OwnsMany(p => p.ChatMessages);



            modelBuilder.Entity<BaseAbility>().HasData(Seeds.BaseAbilities.Get());
            modelBuilder.Entity<BaseItem>().HasData(Seeds.BaseItems.Get());
            modelBuilder.Entity<BaseNpc>().HasData(Seeds.BaseNpcs.Get());
            modelBuilder.Entity<MapField>().HasData(Seeds.MapFields.Get());
            modelBuilder.Entity<Player>().HasData(Seeds.Players.Get());
            //modelBuilder.Entity<PlayerItem>().HasData(Seeds.PlayerItems.Get());
        }
    }
}
