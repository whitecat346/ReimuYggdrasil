using System.Text.Json.Serialization;

namespace ReimuYggdrasil.Core.Models.Server.Requests.AuthServer;

public record InvalidateReq
{
    [JsonPropertyName("accessToken")]
    public required string AccessToken { get; init; }

    [JsonPropertyName("clientToken")]
    public required string ClietnToken { get; init; }
}
