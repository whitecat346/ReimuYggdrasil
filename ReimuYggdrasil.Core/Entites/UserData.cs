using ReimuYggdrasil.Core.Models.Yggdrasil;

namespace ReimuYggdrasil.Core.Entites;

public class UserData : IDisposable
{
    private HashSet<UserInfo> Users { get; } = [];
    private readonly ReaderWriterLockSlim _rwLock = new();

    public void AddUser(string name, string pwd)
    {
        var user = new UserInfo
        {
            Id = name,
            Properties =
            [
                new UserInfoProperty
                {
                    Name = "Password",
                    Value = pwd
                }
            ]
        };

        _rwLock.EnterWriteLock();
        try
        {
            Users.Add(user);
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }

    public UserInfo? GetUser(string name)
    {
        _rwLock.EnterReadLock();
        try
        {
            return Users.FirstOrDefault(user => user.Id.Equals(name, StringComparison.Ordinal));
        }
        finally
        {
            _rwLock.ExitReadLock();
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _rwLock.Dispose();
    }
}
