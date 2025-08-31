using FastEndpoints;

namespace ReimuYggdrasil.Core.Models.Server.Requests.Profiles;

public record TextureReq
{
    [BindFrom("hash")]
    public required string Hash { get; set; }
}
