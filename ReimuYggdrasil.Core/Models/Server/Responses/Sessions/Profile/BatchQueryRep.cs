using ReimuYggdrasil.Core.Models.Yggdrasil;

namespace ReimuYggdrasil.Core.Models.Server.Responses.Sessions.Profile;

public record BatchQueryRep
{
    public List<ProfileInfo> Profiles { get; set; } = [];
}
