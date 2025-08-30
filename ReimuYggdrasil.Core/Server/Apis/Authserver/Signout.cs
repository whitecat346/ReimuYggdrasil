using FastEndpoints;
using ReimuYggdrasil.Core.Entites;
using ReimuYggdrasil.Core.Models.Server.Requests.AuthServer;

namespace ReimuYggdrasil.Core.Server.Apis.Authserver;

public class Signout(
    TokenData tokenData,
    UserData userData
) : Endpoint<SignoutReq>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Post("/authserver/signout");
        Throttle(
            hitLimit: 10,
            durationSeconds: 60
        );
        AllowAnonymous();
    }

    /// <inheritdoc />
    public override async Task HandleAsync(SignoutReq req, CancellationToken ct)
    {
        var username = req.UserName;
        var pwd = req.Password;

        var userInfo = userData.GetUser(username);

        if (userInfo == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var tPwd = userInfo.Properties.First(pro => pro.Name.Equals("Password", StringComparison.Ordinal));
        if (!tPwd.Value.Equals(pwd))
        {
            await Send.ForbiddenAsync(ct);
            return;
        }

        tokenData.InvalidateTokenByName(username);

        await Send.NoContentAsync(ct);
    }
}
