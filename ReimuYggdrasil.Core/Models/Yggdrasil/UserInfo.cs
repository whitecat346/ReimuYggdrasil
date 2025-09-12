using System.Text.Json.Serialization;

namespace ReimuYggdrasil.Core.Models.Yggdrasil;

public record UserInfo
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public required string Password { get; set; }

    [JsonPropertyName("properties")]
    public List<UserInfoProperty> Properties { get; set; } = [];
}

public record UserInfoProperty
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("value")]
    public required string Value { get; set; }
}