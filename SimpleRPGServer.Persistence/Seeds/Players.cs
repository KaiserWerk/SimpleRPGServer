using SimpleRPGServer.Persistence.Models.Ingame;

namespace SimpleRPGServer.Persistence.Seeds;

public static class Players
{
    public static Player[] Get()
    {
        return new Player[]
        {
            new Player("the@dude.com", "TheDude", "test")
            {
                Id = 1,
                Locked = false,
            },
        };
    }
}
