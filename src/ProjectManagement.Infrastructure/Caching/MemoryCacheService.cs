using Microsoft.Extensions.Caching.Memory;

namespace ProjectManagement.Infrastructure.Caching;

public class MemoryCacheService
{
    private readonly IMemoryCache _cache;

    public MemoryCacheService(IMemoryCache cache) => _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    public T? Get<T>(string key)
    {
        if (_cache.TryGetValue(key, out object? obj) && obj is T value)
            return value;

        return default;
    }
    public void Set<T>(string key, T value, TimeSpan? expiration = null)
    {
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(5)
        };
        _cache.Set(key, value, options);
    }

    public void Remove(string key) => _cache.Remove(key);
}
