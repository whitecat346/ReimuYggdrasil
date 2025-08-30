using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReimuYggdrasil.Core.Entites;
using ReimuYggdrasil.Core.Services;

namespace ReimuYggdrasil.Core;

public static class ReimuYggdrasil
{
    private static IHost ConfigureHost(short port)
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddFastEndpoints()
               .AddSingleton<RsaService>()
               .AddSingleton<Sha1WithRsaService>()
               .AddSingleton<TokenService>()
               .AddSingleton<JoinSessionData>()
               .AddSingleton<ProfileData>()
               .AddSingleton<TextureData>()
               .AddSingleton<TokenData>()
               .AddSingleton<UserData>();

        builder.WebHost.UseUrls($"http://localhost:{port}");

        return builder.Build();
    }

    public static void RunServer(short port)
    {
        var app = ConfigureHost(port);
        app.Run();
    }
}
