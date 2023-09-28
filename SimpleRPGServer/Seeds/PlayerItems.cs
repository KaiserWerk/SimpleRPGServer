using SimpleRPGServer.Models.Ingame;

namespace SimpleRPGServer.Seeds
{
    public class PlayerItems
    {
        public static PlayerItem[] Get()
        {
            return new PlayerItem[] 
            { 
                new PlayerItem()
                {
                    Id = 1,
                    Player = new Player() {Id = 1},
                    BaseItem = new BaseItem() {Id = 1},
                    Name = "Rusty Sword",
                    Description = "Just a boring old sword with a lot of rust.",
                    CurrentDurability = 867,
                    MaxDurability = 1000,
                    AttackStrength = 6,
                    DefenseStrength = 0,
                    Charges = 0,
                    Location = ItemLocation.Inventory,
                    X = 0,
                    Y = 0,
                },
            };
        }
    }
}
