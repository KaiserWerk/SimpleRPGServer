namespace SimpleRPGServer.Persistence.Models.Ingame;

public class BaseAbility
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int MaxLevel { get; set; }
    public TimeSpan TrainingDuration { get; set; }
}
