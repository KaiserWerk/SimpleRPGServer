namespace SimpleRPGServer.Persistence.Models.Ingame;

public class Clan
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long Gold { get; set; }

    public virtual ICollection<Player> Players { get; set; }
}
