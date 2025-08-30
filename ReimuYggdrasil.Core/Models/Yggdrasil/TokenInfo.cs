namespace ReimuYggdrasil.Core.Models.Yggdrasil;

public enum TokenState
{
    Active,
    Suspend,
    Expired
}

public record TokenInfo
{
    public required string UserName { get; init; }
    public required string AccessToken { get; init; }
    public required string ClientToken { get; init; }
    public required string BindUuid { get; init; }
    public required DateTime PublicTime { get; init; }
    public required DateTime ExpireAt { get; init; }
    public required TokenState State { get; set; } = TokenState.Active;
}
