namespace SimpleRPGServer.Persistence.Models.Ingame;

public class DroppedGold
{
    public ulong Id { get; set; }
    public long Amount { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}
