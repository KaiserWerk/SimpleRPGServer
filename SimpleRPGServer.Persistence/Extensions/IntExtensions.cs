namespace SimpleRPGServer.Persistence.Extensions;

public static class IntExtensions
{
    public static bool Between(this int a, int b, int c)
    {
        return a >= b && a <= c;
    }
}
