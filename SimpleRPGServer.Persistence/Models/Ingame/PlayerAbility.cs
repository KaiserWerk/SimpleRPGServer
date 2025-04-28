namespace SimpleRPGServer.Persistence.Models.Ingame;

public class PlayerAbility
{
    public ulong Id { get; set; }
    public int CurrentLevel { get; set; }
    public bool Queued { get; set; }

    public virtual Player Player { get; set; }
    public virtual BaseAbility BaseAbility { get; set; }

    public PlayerAbilityData ToApiData()
    {
        return new PlayerAbilityData()
        {
            Id = this.Id,
            Name = this.BaseAbility.Name,
            Description = this.BaseAbility.Description,
            CurrentLevel = this.CurrentLevel,
        };
    }
}

public class PlayerAbilityData
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CurrentLevel { get; set; }
}
