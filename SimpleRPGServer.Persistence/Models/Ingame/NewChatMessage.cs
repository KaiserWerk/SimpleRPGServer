using System.Text.Json.Serialization;

namespace SimpleRPGServer.Persistence.Models.Ingame;

public class NewChatMessage
{
    [JsonPropertyName("message")]
    public string Message { get; set; }
}
