using System.Text.Json.Serialization;

namespace ReimuYggdrasil.Core.Models.Server.Responses;

public record ApiMetaInfoRep
{
    [JsonPropertyName("meta")]
    public ServerMetaInfo Meta { get; } = new();

    [JsonPropertyName("skinDomains")]
    public List<string> SkinDomains { get; } = ["127.0.0.1"];

    [JsonPropertyName("signaturePublickey")]
    public required string Publickey { get; init; }
}

public record ServerMetaInfo
{
    [JsonPropertyName("serverName")]
    public string ServerName { get; } = "ReimuYggdrasil";

    [JsonPropertyName("implementationName")]
    public string ImplementationName { get; } = "yggdrasil";

    [JsonPropertyName("implementationVersion")]
    public string ImplementationVersion { get; } = "1.0.0";

    [JsonPropertyName("feature.non_email_login")]
    public bool NonEmailLogin { get; } = true;
}
