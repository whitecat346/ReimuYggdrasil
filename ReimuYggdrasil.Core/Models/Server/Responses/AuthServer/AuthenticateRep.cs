using System.Text.Json.Serialization;
using ReimuYggdrasil.Core.Models.Yggdrasil;

namespace ReimuYggdrasil.Core.Models.Server.Responses.AuthServer;

public record AuthenticateRep
{
    [JsonPropertyName("accessToken")]
    public required string AccessToken { get; init; }

    [JsonPropertyName("clientToken")]
    public required string ClientToken { get; init; }

    [JsonPropertyName("availableProfiles")]
    public List<ProfileInfo> AvailableProfiles { get; init; } = [];

    [JsonPropertyName("selectedProfile")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ProfileInfo? SeletedProfile { get; init; }

    [JsonPropertyName("user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public UserInfo? User { get; set; }
}
