using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleRPGServer.Persistence.Models.Ingame;

public class BaseItem
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ItemType Type { get; set; }
    public int GoldCost { get; set; }
    public int Durability { get; set; }
    public int AttackStrength { get; set; }
    public int DefenseStrength { get; set; }
    public int MaxCharges { get; set; }

}
