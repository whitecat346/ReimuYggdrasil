using FastEndpoints;
using ReimuYggdrasil.Core.Entites;
using ReimuYggdrasil.Core.Models;
using ReimuYggdrasil.Core.Models.Server.Contexts;
using ReimuYggdrasil.Core.Models.Server.Requests.Sessions;
using ReimuYggdrasil.Core.Models.Yggdrasil;

namespace ReimuYggdrasil.Core.Server.Apis.Sessions;

// Note: we do not trach the client ip and server ip
// because this is used for PCL2-CE, not a formal server

public class Join(
    JoinSessionData joinSessionData,
    TokenData tokenData
) : Endpoint<JoinReq>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Post("/sessionserver/session/minecraft/join");
        AllowAnonymous();
        SerializerContext<SessionJoinContext>();
    }

    /// <inheritdoc />
    public override async Task HandleAsync(JoinReq req, CancellationToken ct)
    {
        var accessToken = req.AccessToken;
        var seletedUuid = req.ProfileUuid;
        var serverId = req.ServerId;

        var tokenInfo = tokenData.GetToken(accessToken);
        if (tokenInfo is not { State: TokenState.Active })
        {
            await Send.ForbiddenAsync(ct);
            return;
        }

        if (!tokenInfo.BindUuid.Equals(seletedUuid, StringComparison.Ordinal))
        {
            await Send.ForbiddenAsync(ct);
            return;
        }

        var session = new JoinSessionInfo
        {
            AccessToken = accessToken,
            ServerId = serverId,
            Uuid = seletedUuid
        };

        joinSessionData.Add(session);

        await Send.NoContentAsync(ct);
    }
}
