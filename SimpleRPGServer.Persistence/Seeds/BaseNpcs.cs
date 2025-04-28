using SimpleRPGServer.Persistence.Models.Ingame;

namespace SimpleRPGServer.Persistence.Seeds;

public static class BaseNpcs
{
    public static BaseNpc[] Get()
    {
        return
        [
            new BaseNpc()
            {
                Id = 1,
                Name = "Wild Dog",
                Description = "A wild stray dog you might encounter on a dirty road.",
                GrantExperience = 1,
                AttackStrength = 2,
                Health = 6,
                GoldDrop = 31,
                SpawnXStart = 20,
                SpawnYStart = 20,
                SpawnXEnd = 24,
                SpawnYEnd = 24,
            },
        ];
    }
}
