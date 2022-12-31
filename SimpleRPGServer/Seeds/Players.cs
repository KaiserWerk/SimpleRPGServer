using SimpleRPGServer.Models.Ingame;

namespace SimpleRPGServer.Seeds
{
    public static class Players
    {
        public static Player[] Get()
        {
            return new Player[]
            {
                new Player("the@dude.com", "TheDude", "p@ssw0rd")
                {
                    Id = 1,
                },
            };
        }
    }
}
