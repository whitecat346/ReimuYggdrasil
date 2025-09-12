using FastEndpoints;
using ReimuYggdrasil.Core.Entites;
using ReimuYggdrasil.Core.Models.Server.Contexts;
using ReimuYggdrasil.Core.Models.Server.Requests.AuthServer;
using ReimuYggdrasil.Core.Models.Server.Responses.AuthServer;
using ReimuYggdrasil.Core.Services;

namespace ReimuYggdrasil.Core.Server.Apis.Authserver;

public class BindUser(
    ProfileData profileData,
    TokenService tokenService,
    TokenData tokenData) : Endpoint<BindUserReq>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Post("/authserver/binduser/{name}");
        AllowAnonymous();
        SerializerContext<BindUserContext>();
    }

    /// <inheritdoc />
    public override async Task HandleAsync(BindUserReq req, CancellationToken ct)
    {
        var targetName = req.Name;

        var profile = profileData.GetProfile(targetName);
        if (profile == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var accessToken = tokenService.GenerateToken();
        var clientToken = Guid.NewGuid().ToString().Replace("-", string.Empty);

        tokenData.AddToken(accessToken, clientToken, targetName, profile.Uuid);

        var rep = new BindUserRep
        {
            AccessToken = accessToken,
            ClientToken = clientToken
        };

        await Send.OkAsync(rep, ct);
    }
}