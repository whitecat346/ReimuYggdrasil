using System.Text.Json.Serialization;
using ReimuYggdrasil.Core.Models.Yggdrasil;

namespace ReimuYggdrasil.Core.Models.Server.Responses.AuthServer;

public record RefreshRep
{
    [JsonPropertyName("accessToken")]
    public required string AccessToken { get; set; }

    [JsonPropertyName("clientToken")]
    public required string ClientToken { get; set; }

    [JsonPropertyName("selectedProfile")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ProfileInfo? SeletcedProfile { get; set; }

    [JsonPropertyName("user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public UserInfo? User { get; set; }
}
