using SimpleRPGServer.Models.Ingame;

namespace SimpleRPGServer.Seeds
{
    public static class BaseNpcs
    {
        public static BaseNpc[] Get()
        {
            return new BaseNpc[] 
            { 
                new BaseNpc()
                {
                    Id = 1,
                    Name = "Wild Dog",
                    Description = "A wild stray dog you might encounter on a dirty road. Just like right now.",
                    GrantExperience = 1,
                    AttackStrength = 2,
                    Health = 6,
                    GoldDrop = 31,
                    SpawnXStart = 20,
                    SpawnYStart = 20,
                    SpawnXEnd = 24,
                    SpawnYEnd = 24,
                },
            };
        }
    }
}
