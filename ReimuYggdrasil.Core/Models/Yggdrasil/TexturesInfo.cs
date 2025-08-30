using System.Text.Json.Serialization;

namespace ReimuYggdrasil.Core.Models.Yggdrasil;

public record TexturesInfo
{
    [JsonPropertyName("timestamp")]
    public long TimeStamp { get; set; }

    [JsonPropertyName("profileId")]
    public required string ProfileId { get; set; }

    [JsonPropertyName("profileName")]
    public required string ProfileName { get; set; }

    [JsonPropertyName("textures")]
    public required SkinTextureInfo Textures { get; set; }
}

public record SkinTextureInfo
{
    [JsonPropertyName("SKIN")]
    public required SkinInfo Skin { get; set; }
}

public record SkinInfo
{
    [JsonPropertyName("url")]
    public required string Url { get; set; }

    [JsonPropertyName("metadata")]
    public required SkinMetadataInfo Metadata { get; set; }
}

public record SkinMetadataInfo
{
    [JsonPropertyName("model")]
    public required string Model { get; set; }
}
