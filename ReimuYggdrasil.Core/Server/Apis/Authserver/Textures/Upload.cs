using FastEndpoints;
using ReimuYggdrasil.Core.Entites;
using ReimuYggdrasil.Core.Models.Server.Requests.User;

namespace ReimuYggdrasil.Core.Server.Apis.Authserver.Textures;

public class Upload(
    ProfileData profileData,
    TokenData tokenData
) : Endpoint<UploadTextureReq>
{
    public override void Configure()
    {
        Put("/api/user/profile/{uuid}/{textureType}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UploadTextureReq req, CancellationToken ct)
    {
        if (req.Authorization == null || !tokenData.ValidateToken(req.Authorization))
        {
            await Send.UnauthorizedAsync(ct);
            return;
        }

        if (req.ContentType is not "multipart/form-data")
        {
            await Send.ForbiddenAsync(ct);
            return;
        }

        var type = req.TextureType;

        if (!type.Equals("skin", StringComparison.Ordinal))
        {
            await Send.ForbiddenAsync(ct);
            return;
        }

        if (!req.File.ContentType.Equals("image/png", StringComparison.Ordinal))
        {
            await Send.ForbiddenAsync(ct);
            return;
        }

        var uuid = req.Uuid;

        using var ms = new MemoryStream();
        await req.File.CopyToAsync(ms, ct);

        var bytes = ms.ToArray();
        var model = string.IsNullOrEmpty(req.Model) ? "default" : req.Model;
        profileData.UploadTexture(uuid, model, bytes);

        await Send.NoContentAsync(ct);
    }
}
