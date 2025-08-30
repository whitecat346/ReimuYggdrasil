using System.Text.Json.Serialization;

namespace ReimuYggdrasil.Core.Models.Yggdrasil;

public record ProfileInfo
{
    [JsonPropertyName("id")]
    public required string Uuid { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("properties")]
    public List<ProfileInfoProperty> Properties { get; set; } = [];
}

public record ProfileInfoProperty
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("value")]
    public required string Value { get; set; }

    [JsonPropertyName("signature")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Signature { get; set; }
}