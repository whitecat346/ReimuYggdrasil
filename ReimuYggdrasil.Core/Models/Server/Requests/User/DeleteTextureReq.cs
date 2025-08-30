using FastEndpoints;

namespace ReimuYggdrasil.Core.Models.Server.Requests.User;

public record DeleteTextureReq
{
    [FromHeader(IsRequired = true)]
    public string? Authorization { get; set; }

    [BindFrom("textureType")]
    public required string TextureType { get; set; }

    [BindFrom("uuid")]
    public required string Uuid { get; set; }
}
