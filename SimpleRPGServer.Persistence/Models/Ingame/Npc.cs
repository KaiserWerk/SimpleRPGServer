namespace SimpleRPGServer.Persistence.Models.Ingame;

public class Npc
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CurrentHealth { get; set; }
    public int AttackStrength { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public virtual BaseNpc BaseNpc { get; set; }

    public Npc()
    { }

    public Npc(BaseNpc baseNpc, int x, int y)
    {
        this.BaseNpc = baseNpc;
        this.Name = baseNpc.Name;
        this.Description = baseNpc.Description;
        this.CurrentHealth = baseNpc.Health;
        this.AttackStrength = baseNpc.AttackStrength;
        this.X = x;
        this.Y = y;
    }
}
