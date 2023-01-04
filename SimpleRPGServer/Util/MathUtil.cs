using SimpleRPGServer.Extensions;
using System;

namespace SimpleRPGServer.Util
{
    public static class MathUtil
    {
        public static bool HappensByChance(int percentage)
        {
            if (percentage >= 100)
                return true;

            Random random = new Random();
            var n = random.Next(0, 101);
            return n.Between(0, percentage);
        }
    }
}
