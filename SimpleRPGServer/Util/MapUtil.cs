using System;

namespace SimpleRPGServer.Util
{
    public static class MapUtil
    {
        public static (int, int) RandomCoordinatesFromRange((int, int) start, (int, int) end)
        {
            Random random = new Random();
            var x = random.Next(start.Item1, end.Item1);
            var y = random.Next(start.Item2, end.Item2);
            return (x, y);
        }
    }
}
