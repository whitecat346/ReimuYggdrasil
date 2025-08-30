using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastEndpoints;
using ReimuYggdrasil.Core.Entites;
using ReimuYggdrasil.Core.Models.Server.Requests.Sessions;
using ReimuYggdrasil.Core.Models.Yggdrasil;
using ReimuYggdrasil.Core.Services;

namespace ReimuYggdrasil.Core.Server.Apis.Sessions;

public class HasJoin(
    JoinSessionData joinSessionData,
    ProfileData profileData,
    Sha1WithRsaService sha1WithRsaService
) : Endpoint<HasJoinReq>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Get("/sessionserver/session/minecraft/hasJoined");
        AllowAnonymous();
    }

    /// <inheritdoc />
    public override async Task HandleAsync(HasJoinReq req, CancellationToken ct)
    {
        var username = req.UserName;
        var serverId = req.ServerId;

        var session = joinSessionData.Get(serverId);
        if (session == null)
        {
            await Send.NoContentAsync(ct);
            return;
        }

        var profile = profileData.GetProfile(username);
        if (profile == null)
        {
            await Send.NoContentAsync(ct);
            return;
        }

        if (session.Uuid != profile.Uuid)
        {
            await Send.NoContentAsync(ct);
            return;
        }

        var texturePro = profile.Properties.First(pro => pro.Name.Equals("textures", StringComparison.Ordinal));
        var signContent = sha1WithRsaService.GetBase64(texturePro.Value);
        texturePro.Signature = signContent;

        var profileWithSign = new ProfileInfo
        {
            Name = profile.Name,
            Uuid = profile.Uuid,
            Properties =
            [
                texturePro
            ]
        };

        await Send.OkAsync(profileWithSign, ct);
    }
}
