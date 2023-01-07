using Newtonsoft.Json;

namespace SimpleRPGServer.Models.Ingame
{
    public class Npc
    {
        public long Id { get; set; }
        public BaseNpc BaseNpc { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CurrentHealth { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Npc()
        {}
        public Npc(BaseNpc baseNpc, int x, int y) 
        { 
            this.BaseNpc = baseNpc;
            this.Name = baseNpc.Name;
            this.Description = baseNpc.Description;
            this.CurrentHealth = baseNpc.Health;
            this.X = x;
            this.Y = y;
        }
    }
}
