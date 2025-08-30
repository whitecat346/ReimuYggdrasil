using System.Text.Json.Serialization;

namespace ReimuYggdrasil.Core.Models.Server.Requests.Sessions.Profile;

public record QueryReq
{
    [JsonPropertyName("uuid")]
    public required string Uuid { get; set; }

    [JsonPropertyName("usigned")]
    public bool Unsigned { get; set; } = true;
}
