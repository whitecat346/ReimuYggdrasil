namespace ReimuYggdrasil.Core.Services;

public class TokenService(Sha1WithRsaService sha1WithRsaService)
{
    public string GenerateToken()
    {
        var guid = Guid.NewGuid();
        var token = guid.ToString().Replace('-', ' ');

        var res = sha1WithRsaService.GetBase64(token);

        return res;
    }
}
