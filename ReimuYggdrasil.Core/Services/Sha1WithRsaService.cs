namespace ReimuYggdrasil.Core.Services;

public class Sha1WithRsaService(RsaService rsaService)
{
    public string GetBase64(string data)
    {
        var singed = rsaService.SingData(data);
        var res = Convert.ToBase64String(singed);
        return res;
    }
}
