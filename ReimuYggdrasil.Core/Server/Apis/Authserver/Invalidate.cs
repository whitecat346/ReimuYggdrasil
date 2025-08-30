using FastEndpoints;
using ReimuYggdrasil.Core.Entites;
using ReimuYggdrasil.Core.Models.Server.Requests.AuthServer;

namespace ReimuYggdrasil.Core.Server.Apis.Authserver;

public class Invalidate(TokenData tokenData) : Endpoint<InvalidateReq>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Post("/authserver/invalidate");
        AllowAnonymous();
    }

    /// <inheritdoc />
    public override async Task HandleAsync(InvalidateReq req, CancellationToken ct)
    {
        var accessToken = req.AccessToken;
        // ignore client token

        tokenData.InvalidateToken(accessToken);

        await Send.NoContentAsync(ct);
    }
}
