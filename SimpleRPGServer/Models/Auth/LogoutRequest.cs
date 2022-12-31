using System.Text.Json.Serialization;

namespace SimpleRPGServer.Models.Auth
{
    public class LogoutRequest
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
