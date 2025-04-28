namespace SimpleRPGServer.Persistence.Models.Ingame;

public class PlayerItem
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CurrentDurability { get; set; }
    public int MaxDurability { get; set; }
    public int AttackStrength { get; set; }
    public int DefenseStrength { get; set; }
    public int Charges { get; set; }
    public bool Equipped { get; set; }
    public ItemLocation Location { get; set; }
    public ItemType ItemType { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public virtual Player Player { get; set; }
    public virtual BaseItem BaseItem { get; set; }

    public PlayerItem()
    { }

    public PlayerItem(Player player, BaseItem baseItem, ItemLocation location)
    {
        this.Player = player;
        this.BaseItem = baseItem;
        this.Location = location;
    }

    public PlayerItem(Player player, BaseItem baseItem, int x, int y)
    {
        this.Player = player;
        this.BaseItem = baseItem;
        this.Location = ItemLocation.Dropped;
        this.X = x;
        this.Y = y;
    }
}
