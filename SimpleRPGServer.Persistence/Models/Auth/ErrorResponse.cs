using System.Text.Json.Serialization;

namespace SimpleRPGServer.Persistence.Models.Auth;

public class ErrorResponse
{
    [JsonPropertyName("error")]
    public string Error { get; set; }
    [JsonPropertyName("error_message")]
    public string ErrorMessage { get; set; }
}
