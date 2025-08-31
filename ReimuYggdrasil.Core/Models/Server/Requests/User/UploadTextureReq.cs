using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace ReimuYggdrasil.Core.Models.Server.Requests.User;

public record UploadTextureReq
{
    [FromHeader(IsRequired = true)]
    public string? Authorization { get; set; }

    [FromHeader("Content-Type", IsRequired = true)]
    public string? ContentType { get; set; }

    public string? Model { get; set; }
    public required IFormFile File { get; set; }

    [BindFrom("uuid")]
    public required string Uuid { get; set; }

    [BindFrom("textureType")]
    public required string TextureType { get; set; }
}
