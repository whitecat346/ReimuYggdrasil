using FastEndpoints;
using ReimuYggdrasil.Core.Entites;
using ReimuYggdrasil.Core.Models.Server.Requests.AuthServer;
using ReimuYggdrasil.Core.Models.Server.Responses.AuthServer;
using ReimuYggdrasil.Core.Services;

namespace ReimuYggdrasil.Core.Server.Apis.Authserver;

public class Authenticate(
    UserData user,
    TokenData tokenData,
    ProfileData profileData,
    TokenService tokenService
) : Endpoint<AuthenticateReq, AuthenticateRep>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Post("/authserver/authenticate");
        Throttle(
            hitLimit: 10,
            durationSeconds: 60
        );
        AllowAnonymous();
    }

    /// <inheritdoc />
    public override async Task HandleAsync(AuthenticateReq req, CancellationToken ct)
    {
        var username = req.UserName;
        var pwd = req.Password;

        var userInfo = user.GetUser(username);

        if (userInfo == null)
        {
            await Send.ForbiddenAsync(ct);
            return;
        }

        var userPwd = userInfo.Properties.First(it => it.Name.Equals("Password", StringComparison.Ordinal));
        if (!userPwd.Value.Equals(pwd, StringComparison.Ordinal))
        {
            await Send.ForbiddenAsync(ct);
            return;
        }

        var token = tokenService.GenerateToken();
        var clientToken = string.IsNullOrEmpty(req.ClientToken) ? tokenService.GenerateToken() : req.ClientToken;

        var profile = profileData.GetProfile(username);
        if (profile == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var tokenInfo = tokenData.AddToken(token, clientToken, username, profile.Uuid);

        var rep = new AuthenticateRep
        {
            AccessToken = tokenInfo.AccessToken,
            ClientToken = tokenInfo.ClientToken,
            SeletedProfile = profile
        };

        if (req.RequestUser)
        {
            rep.User = userInfo;
        }

        await Send.OkAsync(rep, ct);

        return;
    }
}
