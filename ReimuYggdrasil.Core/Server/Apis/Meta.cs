using FastEndpoints;
using ReimuYggdrasil.Core.Models.Server.Contexts;
using ReimuYggdrasil.Core.Models.Server.Responses;
using ReimuYggdrasil.Core.Services;

namespace ReimuYggdrasil.Core.Server.Apis;

public class Meta(RsaService rsaService) : Ep.NoReq.Res<ApiMetaInfoRep>
{
    public override void Configure()
    {
        Post("/");
        AllowAnonymous();
        SerializerContext<ApiMetaInfoContext>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var metaInfo = new ApiMetaInfoRep
        {
            Publickey = rsaService.PubKey
        };

        await Send.OkAsync(metaInfo, ct);
    }
}
