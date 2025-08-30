using System.Text.Json.Serialization;

namespace ReimuYggdrasil.Core.Models.Server.Requests.AuthServer;

public record SignoutReq
{
    [JsonPropertyName("username")]
    public required string UserName { get; set; }

    [JsonPropertyName("pssword")]
    public required string Password { get; set; }
}
