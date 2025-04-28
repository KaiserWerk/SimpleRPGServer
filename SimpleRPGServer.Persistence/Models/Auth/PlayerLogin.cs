using SimpleRPGServer.Persistence.Models.Ingame;
using System.Text.Json.Serialization;

namespace SimpleRPGServer.Persistence.Models.Auth;

public class PlayerLogin
{
    [JsonIgnore]
    public ulong Id { get; set; }
    [JsonIgnore]
    public ulong PlayerId { get; set; }
    [JsonPropertyName("token")]
    public string Token { get; set; }
    [JsonPropertyName("valid_until")]
    public DateTime ValidUntil { get; set; }

    public virtual PlayerData PlayerData { get; set; }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(this.Token) && this.ValidUntil > DateTime.UtcNow;
    }
}
