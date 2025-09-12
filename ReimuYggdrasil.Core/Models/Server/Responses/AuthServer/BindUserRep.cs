using System.Text.Json.Serialization;

namespace ReimuYggdrasil.Core.Models.Server.Responses.AuthServer;

public record BindUserRep
{
    [JsonPropertyName("accessToken")]
    public required string AccessToken { get; init; }

    [JsonPropertyName("clientToken")]
    public required string ClientToken { get; init; }
}
