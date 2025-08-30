using FastEndpoints;
using ReimuYggdrasil.Core.Entites;
using ReimuYggdrasil.Core.Models.Server.Requests.AuthServer;
using ReimuYggdrasil.Core.Models.Server.Responses.AuthServer;

namespace ReimuYggdrasil.Core.Server.Apis.Authserver;

public class Refresh(
    TokenData tokenData,
    UserData userData,
    ProfileData profileData
) : Endpoint<RefreshReq, RefreshRep>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Post("/authserver/refresh");
        AllowAnonymous();
    }

    /// <inheritdoc />
    public override async Task HandleAsync(RefreshReq req, CancellationToken ct)
    {
        var accessToken = req.AccessToken;
        var clientToken = string.IsNullOrEmpty(req.ClietnToken) ? string.Empty : req.ClietnToken;

        var info = tokenData.RefreshToken(accessToken, clientToken);
        if (info == null)
        {
            await Send.ForbiddenAsync(ct);
            return;
        }

        var rep = new RefreshRep
        {
            AccessToken = info.AccessToken,
            ClientToken = info.ClientToken,
            SeletcedProfile = profileData.GetProfile(info.UserName)
        };

        if (req.RequestUser)
        {
            var user = userData.GetUser(info.UserName);
            if (user == null)
            {
                await Send.NotFoundAsync(ct);
                return;
            }

            rep.User = user;
        }

        await Send.OkAsync(rep, ct);
    }
}
