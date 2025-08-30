using System.Text.Json.Serialization;

namespace ReimuYggdrasil.Core.Models.Server.Requests.AuthServer;

public record AuthenticateReq
{
    [JsonPropertyName("username")]
    public required string UserName { get; set; }

    [JsonPropertyName("pssword")]
    public required string Password { get; set; }

    [JsonPropertyName("clientToken")]
    public string? ClientToken { get; set; }

    [JsonPropertyName("requestUser")]
    public bool RequestUser { get; set; } = false;

    [JsonPropertyName("agent")]
    public AuthenticateReqAgentInfo Agent { get; set; } = new();
}

public record AuthenticateReqAgentInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "Minecraft";

    [JsonPropertyName("version")]
    public int Version { get; set; } = 1;
}
