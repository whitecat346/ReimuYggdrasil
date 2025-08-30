using System.Text.Json.Serialization;

namespace ReimuYggdrasil.Core.Models.Server.Responses;

public record ErrorRequestRep
{
    [JsonPropertyName("error")]
    public required string Error { get; set; }

    [JsonPropertyName("errorMessage")]
    public required string ErrorMessage { get; set; }

    [JsonPropertyName("cause")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Cause { get; set; }
}
