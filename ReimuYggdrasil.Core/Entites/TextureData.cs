using System.Text.Json;
using ReimuYggdrasil.Core.Models.Yggdrasil;
using ReimuYggdrasil.Core.Utils;

namespace ReimuYggdrasil.Core.Entites;

public class TextureData
{
    private readonly Dictionary<string, byte[]> _uploadedTextures = [];

    public string GenerateTexture(string uuid, string name, string model, byte[] textureBytes)
    {
        var unixTimeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        var url = RuntimeInfo.BaseUri + "/texture/" + HashUtil.ComputeHash(textureBytes);

        var texture = new TexturesInfo
        {
            ProfileId = uuid,
            ProfileName = name,
            TimeStamp = unixTimeStamp,
            Textures = new SkinTextureInfo
            {
                Skin = new SkinInfo
                {
                    Url = url,
                    Metadata = new SkinMetadataInfo
                    {
                        Model = model
                    }
                }
            }
        };

        var textureJson = JsonSerializer.Serialize(texture);
        return textureJson;
    }

    public void StoreTexture(byte[] textureBytes)
    {
        var hash = HashUtil.ComputeHash(textureBytes);
        _uploadedTextures.TryAdd(hash, textureBytes);
    }

    public byte[]? GetTexture(string hash)
    {
        return _uploadedTextures.GetValueOrDefault(hash);
    }
}
