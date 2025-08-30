using FastEndpoints;
using ReimuYggdrasil.Core.Entites;
using ReimuYggdrasil.Core.Models.Server.Requests.AuthServer;

namespace ReimuYggdrasil.Core.Server.Apis.Authserver;

public class Validate(TokenData tokenData) : Endpoint<ValidateReq>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Post("/authserver/validate");
        AllowAnonymous();
    }

    /// <inheritdoc />
    public override async Task HandleAsync(ValidateReq req, CancellationToken ct)
    {
        var accessToken = req.AccessToken;
        var clientTokne = req.ClietnToken;

        var isUseable = tokenData.ValidateToken(accessToken, clientTokne);
        if (isUseable)
        {
            await Send.NoContentAsync(ct);
            return;
        }

        await Send.ForbiddenAsync(ct);
    }
}
