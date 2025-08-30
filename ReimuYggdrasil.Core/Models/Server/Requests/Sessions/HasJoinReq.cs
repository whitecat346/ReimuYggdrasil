namespace ReimuYggdrasil.Core.Models.Server.Requests.Sessions;

public record HasJoinReq
{
    public required string UserName { get; set; }

    public required string ServerId { get; set; }
}
