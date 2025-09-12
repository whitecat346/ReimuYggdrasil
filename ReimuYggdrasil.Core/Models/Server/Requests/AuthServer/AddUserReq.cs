using System.Text.Json.Serialization;

namespace ReimuYggdrasil.Core.Models.Server.Requests.AuthServer;

public record AddUserReq
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("uuid")]
    public required string Uuid { get; init; }

    [JsonPropertyName("textures")]
    public TextureInfo? Textures { get; init; } = null;
}

public record TextureInfo
{
    [JsonPropertyName("skin")]
    public string? Skin { get; init; }

    [JsonPropertyName("cape")]
    public string? Cape { get; init; }
}
