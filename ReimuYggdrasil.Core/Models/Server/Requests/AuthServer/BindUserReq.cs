using FastEndpoints;

namespace ReimuYggdrasil.Core.Models.Server.Requests.AuthServer;

public record BindUserReq
{
    [BindFrom("name")]
    public required string Name { get; init; }
}
