using System.Text.Json.Serialization;

namespace ReimuYggdrasil.Core.Models.Server.Requests.AuthServer;

public record ValidateReq
{
    [JsonPropertyName("accessToken")]
    public required string AccessToken { get; init; }

    [JsonPropertyName("clientToken")]
    public string? ClietnToken { get; init; }
}
