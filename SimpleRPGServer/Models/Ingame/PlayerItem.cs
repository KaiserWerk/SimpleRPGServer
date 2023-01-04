using Microsoft.Build.ObjectModelRemoting;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleRPGServer.Models.Ingame
{
    public class PlayerItem
    {
        public long Id { get; set; }
        public Player Player { get; set; }
        public BaseItem BaseItem { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CurrentDurability { get; set; }
        public int MaxDurability { get; set; }
        public int AttackStrength { get; set; }
        public int DefenseStrength { get; set; }
        public int Charges { get; set; }
        public ItemLocation Location { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public PlayerItem()
        {}

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
}
