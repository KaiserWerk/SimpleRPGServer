namespace SimpleRPGServer.Persistence.Models.Ingame;

public class ItemTable
{
    public ulong Id { get; set; }
    public List<PlayerItem> GetDrops()
    {
        return new List<PlayerItem>();
    }
}


