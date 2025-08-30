using System.Security.Cryptography;
using System.Text;

namespace ReimuYggdrasil.Core.Services;

public class RsaService
{
    private RSA RsaInstance { get; set; } = RSA.Create();

    public string PubKey => RsaInstance.ExportRSAPublicKeyPem();

    public byte[] SingData(string data)
    {
        var dataBytes = Encoding.UTF8.GetBytes(data);

        var singatureBytes = RsaInstance.SignData(dataBytes, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);

        return singatureBytes;
    }
}
