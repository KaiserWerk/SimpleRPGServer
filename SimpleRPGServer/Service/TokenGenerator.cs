using System;
using System.Security.Cryptography;

namespace SimpleRPGServer.Service;

public class TokenGenerator : ITokenGenerator
{
    public string GenerateToken(int length)
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(length));
    }
}
