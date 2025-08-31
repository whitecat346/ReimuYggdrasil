using FastEndpoints;
using ReimuYggdrasil.Core.Entites;
using ReimuYggdrasil.Core.Models.Server.Requests.Profiles;

namespace ReimuYggdrasil.Core.Server.Apis.Authserver.Textures;

public class GetTexture(TextureData textureData) : Endpoint<TextureReq>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Get("/textures/{hash}");
        AllowAnonymous();
    }

    /// <inheritdoc />
    public override async Task HandleAsync(TextureReq req, CancellationToken ct)
    {
        var hash = req.Hash;
        var bytes = textureData.GetTexture(hash);

        if (bytes == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var stream = new MemoryStream(bytes);
        await Send.StreamAsync(stream, fileLengthBytes: bytes.Length, contentType: "image/png", cancellation: ct);
    }
}
