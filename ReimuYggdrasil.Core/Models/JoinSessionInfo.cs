namespace ReimuYggdrasil.Core.Models;

public record JoinSessionInfo
{
    public required string AccessToken { get; init; }
    public required string ServerId { get; init; }
    public required string Uuid { get; init; }
    public DateTime ExpiredAt { get; } = DateTime.Now + TimeSpan.FromSeconds(30);
}
