using System.Text.Json.Serialization;

namespace SimpleRPGServer.Persistence.Models.Auth;

public class LoginRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }

    public bool Valid => !string.IsNullOrWhiteSpace(this.Email) && !string.IsNullOrWhiteSpace(this.Password);
}
