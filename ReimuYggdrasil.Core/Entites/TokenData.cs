using ReimuYggdrasil.Core.Models.Yggdrasil;
using ReimuYggdrasil.Core.Services;

namespace ReimuYggdrasil.Core.Entites;

public class TokenData : IDisposable
{
    private readonly HashSet<TokenInfo> _tokens = [];

    private readonly ReaderWriterLockSlim _rwLock = new();

    private readonly Timer _expireTimer;

    private readonly TokenService _tokenService;

    public TokenData(TokenService tokenService)
    {
        _tokenService = tokenService;
        _expireTimer = new Timer(ExpireToken, null, 0, TimeSpan.TicksPerSecond);
    }

    private void ExpireToken(object? state)
    {
        var currentTime = DateTime.Now;

        _rwLock.EnterWriteLock();
        try
        {
            foreach (var tokenInfo in _tokens)
            {
                if (tokenInfo.State != TokenState.Expired && tokenInfo.ExpireAt < currentTime)
                {
                    tokenInfo.State = TokenState.Expired;
                }
            }
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }

    public bool ValidateToken(string accessToken, string? clientToken = null)
    {
        var token = GetToken(accessToken);
        if (token == null)
        {
            return false;
        }

        if (clientToken != null)
        {
            var isEqual = clientToken.Equals(token.ClientToken, StringComparison.Ordinal);
            if (!isEqual)
            {
                return false;
            }
        }

        if (IsUsable(token))
        {
            return true;
        }

        return false;
    }

    public TokenInfo? RefreshToken(string accessTokne, string clientToken)
    {
        var token = GetToken(accessTokne);
        if (token == null)
        {
            return null;
        }

        if (IsExpired(token))
        {
            return null;
        }

        if (!string.IsNullOrEmpty(clientToken))
        {
            if (!token.ClientToken.Equals(clientToken, StringComparison.Ordinal))
            {
                return null;
            }
        }

        _rwLock.EnterWriteLock();
        try
        {
            token.State = TokenState.Expired;
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }

        var newAccessToken = _tokenService.GenerateToken();

        var newInfo = AddToken(newAccessToken, token.ClientToken, token.UserName, token.BindUuid);

        return newInfo;
    }

    public TokenInfo AddToken(string accessToken, string clientToken, string username, string uuid)
    {
        var info = new TokenInfo
        {
            UserName = username,
            AccessToken = accessToken,
            ClientToken = clientToken,
            BindUuid = uuid,
            State = TokenState.Active,
            PublicTime = DateTime.Now,
            ExpireAt = DateTime.Now + TimeSpan.FromMinutes(30)
        };

        _rwLock.EnterWriteLock();
        try
        {
            _tokens.Add(info);
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }

        return info;
    }

    public TokenInfo? GetToken(string accessToken)
    {
        _rwLock.EnterReadLock();
        try
        {
            return _tokens.FirstOrDefault(info => info.AccessToken.Equals(accessToken, StringComparison.Ordinal));
        }
        finally
        {
            _rwLock.ExitReadLock();
        }
    }

    public void InvalidateToken(string accessToken)
    {
        _rwLock.EnterWriteLock();
        try
        {
            var token = _tokens.FirstOrDefault(info => info.AccessToken.Equals(accessToken, StringComparison.Ordinal));
            if (token != null)
            {
                token.State = TokenState.Expired;
            }
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }

    public void InvalidateTokenByName(string username)
    {
        _rwLock.EnterWriteLock();
        try
        {
            var token = _tokens.FirstOrDefault(info => info.UserName.Equals(username, StringComparison.Ordinal));
            if (token != null)
            {
                token.State = TokenState.Expired;
            }
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }

    private static bool IsUsable(TokenInfo info) =>
        info.State is TokenState.Active;

    private static bool IsExpired(TokenInfo info) =>
        info.State is TokenState.Expired;

    /// <inheritdoc />
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _rwLock.Dispose();
        _expireTimer.Dispose();
    }
}
