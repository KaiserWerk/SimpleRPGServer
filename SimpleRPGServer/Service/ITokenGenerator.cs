namespace SimpleRPGServer.Service
{
    public interface ITokenGenerator
    {
        string GenerateToken(int length);
    }
}