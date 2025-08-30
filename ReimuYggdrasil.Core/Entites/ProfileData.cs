using ReimuYggdrasil.Core.Models.Yggdrasil;
using ReimuYggdrasil.Core.Utils;

namespace ReimuYggdrasil.Core.Entites;

public class ProfileData(TextureData textureData)
{
    public static List<ProfileInfoProperty> DefaultProfileProperties =>
    [
        new()
        {
            Name = "uploadableTextures",
            Value = "skin"
        }
    ];

    private HashSet<ProfileInfo> Profiles { get; set; } = [];
    private readonly ReaderWriterLockSlim _rwLock = new();

    public void AddProfile(ProfileInfo profile)
    {
        _rwLock.EnterWriteLock();
        try
        {
            Profiles.Add(profile);
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }

    public void AddProfile(string name)
    {
        var uuid = UuidUtil.GenerateUuid(name);

        _rwLock.EnterWriteLock();
        try
        {
            Profiles.Add(new ProfileInfo { Name = name, Uuid = uuid });
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }

        var defaultTextures = DefaultSkins.Steve;
        UploadTexture(uuid, name, defaultTextures);
    }

    public void SetDefaultTexture(string name)
    {
        var profile = GetProfile(name);
        if (profile == null)
        {
            return;
        }

        var defaultTextures = DefaultSkins.Steve;
        UploadTexture(profile.Uuid, name, defaultTextures);
    }

    public void RemoveProfile(string name)
    {
        _rwLock.EnterWriteLock();
        try
        {
            Profiles.RemoveWhere(it => it.Name.Equals(name, StringComparison.Ordinal));
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }

    public void UploadTexture(string uuid, string module, byte[] textureBbytes)
    {
        _rwLock.EnterWriteLock();
        try
        {
            var profile = Profiles.FirstOrDefault(it => it.Uuid.Equals(uuid, StringComparison.Ordinal));
            if (profile == null) return;

            var textureBase = textureData.GenerateTexture(profile.Uuid, profile.Name, module, textureBbytes);

            var textureProperty = new ProfileInfoProperty
            {
                Name = "textures",
                Value = textureBase,
            };

            var property =
                profile.Properties.FirstOrDefault(it => it.Name.Equals("textures", StringComparison.Ordinal));
            if (property != null)
            {
                property.Value = textureBase;
            }
            else
            {
                profile.Properties.Add(textureProperty);
            }
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }

    public ProfileInfo? GetProfile(string name)
    {
        _rwLock.EnterReadLock();
        try
        {
            var profile = Profiles.FirstOrDefault(it => it.Name.Equals(name, StringComparison.Ordinal));
            return profile;
        }
        finally
        {
            _rwLock.ExitReadLock();
        }
    }
}
