using Microsoft.EntityFrameworkCore;
using SimpleRPGServer.Models.Ingame;
using SimpleRPGServer.Persistence.Models.Auth;
using SimpleRPGServer.Persistence.Models.Ingame;
using System.Reflection.Metadata;

namespace SimpleRPGServer.Persistence.Models;

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
        modelBuilder.Entity<AuthAction>(opts =>
        {
            opts.HasKey(e => e.Id);
            opts.HasIndex(e => e.Code);
        });

        modelBuilder.Entity<PlayerLogin>(opts =>
        {
            opts.HasKey(e => e.Id);
            opts.HasIndex(e => e.Token);
            opts.Ignore(e => e.PlayerData);
        });


        modelBuilder.Entity<AbilityTraining>(opts =>
        {
            opts.HasKey(e => e.Id);

        });
        modelBuilder.Entity<BaseAbility>(opts =>
        {
            opts.HasKey(e => e.Id);
            opts.HasData(Seeds.BaseAbilities.Get());
        });

        modelBuilder.Entity<BaseItem>(opts =>
        {
            opts.HasKey(e => e.Id);
            opts.HasData(Seeds.BaseItems.Get());
        });

        modelBuilder.Entity<BaseNpc>(opts =>
        {
            opts.HasKey(e => e.Id);
            opts.HasData(Seeds.BaseNpcs.Get());
        });
        modelBuilder.Entity<ChatMessage>(opts =>
        {
            opts.HasKey(e => e.Id);

        });
        modelBuilder.Entity<Clan>(opts =>
        {
            opts.HasKey(e => e.Id);

        });
        modelBuilder.Entity<DroppedGold>(opts =>
        {
            opts.HasKey(e => e.Id);

        });
        // FightResult - no db model
        modelBuilder.Entity<ItemTable>(opts =>
        {
            opts.HasKey(e => e.Id);

        });
        modelBuilder.Entity<MapField>(opts =>
        {
            opts.HasKey(e => e.Id);
            opts.HasData(Seeds.MapFields.Get());
        });
        modelBuilder.Entity<Npc>(opts =>
        {
            opts.HasKey(e => e.Id);
        });

        modelBuilder.Entity<Player>(opts =>
        {
            opts.HasKey(e => e.Id);
            opts.HasIndex(p => p.Email).IsUnique(true);
            opts.HasIndex(p => p.DisplayName).IsUnique(true);

            opts.HasOne(p => p.Clan).WithMany(p => p.Players);
            opts.HasMany(p => p.Abilities).WithOne(p => p.Player);
            opts.HasOne(p => p.AbilityTraining)
                .WithOne(p => p.Player)
                .HasForeignKey<AbilityTraining>(e => e.PlayerId)
                .IsRequired(false); 
            opts.HasMany(p => p.Items).WithOne(p => p.Player);

            opts.HasData(Seeds.Players.Get());
        });

        modelBuilder.Entity<PlayerAbility>(opts =>
        {
            opts.HasKey(e => e.Id);
        });

        modelBuilder.Entity<PlayerItem>(opts => 
        {
            opts.HasKey(e => e.Id);
            
        });
    }
}
