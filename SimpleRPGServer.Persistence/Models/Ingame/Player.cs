using SimpleRPGServer.Models.Ingame;
using System.Text.Json.Serialization;

namespace SimpleRPGServer.Persistence.Models.Ingame;

public class Player
{
    public ulong Id { get; set; }
    public string Email { get; set; }
    public string DisplayName { get; set; }
    public string Password { get; set; }
    public long Gold { get; set; }
    public int ExperiencePoints { get; set; }
    public int MaxExperiencePoints { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public int Strength { get; set; }
    public int Intelligence { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public bool Locked { get; set; }
    public DateTime CreatedAt { get; set; }


    public virtual Clan? Clan { get; set; }
    public virtual ICollection<PlayerAbility> Abilities { get; set; } = new List<PlayerAbility>();
    public virtual AbilityTraining? AbilityTraining { get; set; }
    public virtual ICollection<PlayerItem> Items { get; set; } = new List<PlayerItem>();

    public Player() { }

    public Player(string email, string displayName, string password)
    {
        this.Email = email;
        this.DisplayName = displayName;
        this.Password = password;
        this.MaxExperiencePoints = 10_000_000;
        this.CurrentHealth = 20;
        this.MaxHealth = 20;
        this.Strength = 22;
        this.Intelligence = 18;
        this.Locked = true;
        this.CreatedAt = DateTime.Now;
    }

    public int GetAttackStrength()
    {
        var x = this.Strength;
        var attackItem = this.Items.FirstOrDefault(i => i.ItemType == ItemType.AttackWeapon && i.Equipped);
        if (attackItem != null)
            x += attackItem.AttackStrength;
        return x;
    }

    public int GetDefenseStrength()
    {
        var x = this.ExperiencePoints / 250;
        var defenseItem = this.Items.FirstOrDefault(i => i.ItemType == ItemType.DefenseWeapon && i.Equipped);
        if (defenseItem != null)
            x += defenseItem.DefenseStrength;
        return x;
    }

    public PlayerData ToApiData()
    {
        return new PlayerData()
        {
            Email = this.Email,
            DisplayName = this.DisplayName,
            Gold = this.Gold,
            ExperiencePoints = this.ExperiencePoints,
            MaxExperiencePoints = this.MaxExperiencePoints,
            CurrentHealth = this.CurrentHealth,
            MaxHealth = this.MaxHealth,
            Strength = this.Strength,
            Intelligence = this.Intelligence,
            X = this.X,
            Y = this.Y,
        };
    }
}

public class PlayerData
{
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; }
    [JsonPropertyName("gold")]
    public long Gold { get; set; }
    [JsonPropertyName("experience_points")]
    public int ExperiencePoints { get; set; }
    [JsonPropertyName("max_experience_points")]
    public int MaxExperiencePoints { get; set; }
    [JsonPropertyName("current_health")]
    public int CurrentHealth { get; set; }
    [JsonPropertyName("max_health")]
    public int MaxHealth { get; set; }
    [JsonPropertyName("strength")]
    public int Strength { get; set; }
    [JsonPropertyName("intelligence")]
    public int Intelligence { get; set; }
    [JsonPropertyName("x")]
    public int X { get; set; }
    [JsonPropertyName("y")]
    public int Y { get; set; }
}
