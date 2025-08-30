using System.Text.Json.Serialization;
using ReimuYggdrasil.Core.Models.Yggdrasil;

namespace ReimuYggdrasil.Core.Models.Server.Requests.AuthServer;

public record RefreshReq
{
    [JsonPropertyName("accessToken")]
    public required string AccessToken { get; set; }

    [JsonPropertyName("clientToken")]
    public string? ClietnToken { get; set; }

    [JsonPropertyName("requestUser")]
    public bool RequestUser { get; set; } = false;

    [JsonPropertyName("selectedProfile")]
    public ProfileInfo? SelectedProfile { get; set; }
}
