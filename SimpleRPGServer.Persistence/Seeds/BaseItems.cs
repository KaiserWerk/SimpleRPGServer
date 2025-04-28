using SimpleRPGServer.Persistence.Models.Ingame;

namespace SimpleRPGServer.Persistence.Seeds;

public static class BaseItems
{
    public static BaseItem[] Get()
    {
        return new BaseItem[]
        {
            new BaseItem()
            {
                Id = 1,
                Name = "Rusty Sword",
                Description = "An old and rusty stabbing device which has seen better days.",
                Durability = 350,
                GoldCost = 270,
            },
            new BaseItem()
            {
                Id = 2,
                Name = "Staff of Fire",
                Description = "A small staff that allows you to cast fireballs.",
                Durability = 60,
                GoldCost = 3240,
            }
        };
    }
}
