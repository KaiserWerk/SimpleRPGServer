using SimpleRPGServer.Persistence.Models.Ingame;
using System.Text.Json.Serialization;

namespace SimpleRPGServer.Persistence.Models.Auth;

public class LoginResponse
{
    [JsonIgnore]
    public ulong Id { get; set; }

    [JsonIgnore]
    public ulong PlayerId { get; set; }

    [JsonPropertyName("success")]
    public bool Success { get; set; } = true;

    [JsonPropertyName("technical_code")]
    public TechnicalCode TechnicalCode { get; set; } = TechnicalCode.Ok;

    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;

    [JsonPropertyName("valid_until")]
    public DateTimeOffset ValidUntil { get; set; }

    [JsonPropertyName("player_data")]
    public PlayerData? PlayerData { get; set; }

    public LoginResponse()
    {}

    public LoginResponse(TechnicalCode tecCode)
    {
        this.Success = false;
        this.TechnicalCode = tecCode;
    }

    public LoginResponse(TechnicalCode tecCode, string token, DateTimeOffset validUntil, PlayerData playerData)
    {
        this.Success = true;
        this.Token = token;
        this.ValidUntil = validUntil;
        this.PlayerData = playerData;
    }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(this.Token) && this.ValidUntil > DateTimeOffset.UtcNow;
    }
}
