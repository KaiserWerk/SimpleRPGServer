using SimpleRPGServer.Persistence.Models.Ingame;

namespace SimpleRPGServer.Persistence.Seeds;

public static class BaseAbilities
{
    public static BaseAbility[] Get()
    {
        return new BaseAbility[]
        {
            new BaseAbility()
            {
                Id = 1,
                Name = "Learning technique",
                Description = "This ability makes you train all other abilities 3% faster per level.",
                MaxLevel = 50,
                TrainingDuration = TimeSpan.FromHours(6),
            },
            new BaseAbility()
            {
                Id = 2,
                Name = "Gold mining",
                Description = "Every level grants you 1% more gold when mining in the gold mine.",
                MaxLevel = 30,
                TrainingDuration = TimeSpan.FromHours(2),
            }
        };
    }
}
