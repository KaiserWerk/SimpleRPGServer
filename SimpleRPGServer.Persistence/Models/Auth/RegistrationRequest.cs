using System.Text.Json.Serialization;

namespace SimpleRPGServer.Persistence.Models.Auth;

public class RegistrationRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}
