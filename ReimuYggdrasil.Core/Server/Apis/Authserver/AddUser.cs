using FastEndpoints;
using ReimuYggdrasil.Core.Entites;
using ReimuYggdrasil.Core.Models.Server.Contexts;
using ReimuYggdrasil.Core.Models.Server.Requests.AuthServer;

namespace ReimuYggdrasil.Core.Server.Apis.Authserver;

public class AddUser(
    ProfileData profileData
) : Endpoint<AddUserReq>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Post("/authserver/adduser");
        AllowAnonymous();
        SerializerContext<AddUserContext>();
    }

    private static async Task<byte[]?> GetSkin(string skinFilePath)
    {
        if (!File.Exists(skinFilePath))
        {
            return null;
        }

        await using var fs = new FileStream(skinFilePath, FileMode.Open, FileAccess.Read);
        var skinData = new byte[fs.Length];
        await fs.ReadExactlyAsync(skinData, 0, skinData.Length);
        return skinData;
    }

    /// <inheritdoc />
    public override async Task HandleAsync(AddUserReq req, CancellationToken ct)
    {
        profileData.AddProfile(req.Name, req.Uuid);

        if (req.Textures is { Skin: not null })
        {
            var skinData = await GetSkin(req.Textures.Skin);

            if (skinData == null)
            {
                await Send.NotFoundAsync(ct);
                return;
            }

            profileData.UploadTexture(req.Uuid, req.Name, skinData);
        }

        await Send.NoContentAsync(ct);
    }
}