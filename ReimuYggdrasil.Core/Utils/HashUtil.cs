using System.Security.Cryptography;

namespace ReimuYggdrasil.Core.Utils;

public static class HashUtil
{
    private static readonly ThreadLocal<SHA256> Sha256Local = new(SHA256.Create);

    public static string ComputeHash(byte[] bytes)
    {
        using var sha256 = Sha256Local.Value!;
        var hashBytes = sha256.ComputeHash(bytes);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }
}
