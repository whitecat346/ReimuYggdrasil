namespace ReimuYggdrasil.Core.Models.Server.Requests.Profiles;

public record BatchQueryReq
{
    public List<string> Names { get; set; } = [];
}
