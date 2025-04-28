namespace SimpleRPGServer.Persistence.Models.Ingame;

public class AbilityTraining
{
    public ulong Id { get; set; }
    public DateTimeOffset StartedAt { get; set; }

    public ulong? PlayerId { get; set; }
    public virtual Player? Player { get; set; }
    public virtual PlayerAbility Ability { get; set; }
}
