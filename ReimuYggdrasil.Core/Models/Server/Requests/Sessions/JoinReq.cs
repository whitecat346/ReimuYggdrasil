using System.Text.Json.Serialization;

namespace ReimuYggdrasil.Core.Models.Server.Requests.Sessions;

public record JoinReq
{
    [JsonPropertyName("accessToken")]
    public required string AccessToken { get; init; }

    [JsonPropertyName("selectedProfile")]
    public required string ProfileUuid { get; set; }

    [JsonPropertyName("serverId")]
    public required string ServerId { get; set; }
}
