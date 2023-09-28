using System.Text.Json.Serialization;

namespace SimpleRPGServer.Models.Ingame
{
    public class NewChatMessage
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
