using FastEndpoints;
using ReimuYggdrasil.Core.Entites;
using ReimuYggdrasil.Core.Models.Server.Requests.User;

namespace ReimuYggdrasil.Core.Server.Apis.Authserver.Textures;

public class Delete(
    ProfileData profileData,
    TokenData tokenData
) : Endpoint<DeleteTextureReq>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Delete("/api/user/profile/{uuid}/{textureType}");
        AllowAnonymous();
    }

    /// <inheritdoc />
    public override async Task HandleAsync(DeleteTextureReq req, CancellationToken ct)
    {
        if (req.Authorization == null || !tokenData.ValidateToken(req.Authorization))
        {
            await Send.UnauthorizedAsync(ct);
            return;
        }

        var profile = profileData.GetProfile(req.Uuid);
        if (profile == null)
        {
            await Send.ForbiddenAsync(ct);
            return;
        }

        profileData.SetDefaultTexture(profile.Name);

        await Send.NoContentAsync(ct);
    }
}
