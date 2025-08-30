using System.Security.Cryptography;
using System.Text;

namespace ReimuYggdrasil.Core.Utils;

public static class UuidUtil
{
    private static readonly ThreadLocal<MD5> Md5Instance = new(MD5.Create);

    public static string GenerateUuid(string input)
    {
        var username = "OfflinePlayer: " + input;

        var bytes = Encoding.UTF8.GetBytes(username);
        var hash = Md5Instance.Value!.ComputeHash(bytes);

        // Set version (3) and variant bits
        hash[6] = (byte)((hash[6] & 0x0F) | 0x30); // Version 3
        hash[8] = (byte)((hash[8] & 0x3F) | 0x80); // Variant 1 (RFC 4122)

        return new Guid(hash).ToString("N");
    }
}
