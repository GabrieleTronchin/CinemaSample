using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace ServiceCache;

public class CacheService : ICacheService
{
    private static readonly SemaphoreSlim Locker = new SemaphoreSlim(1, 1);
    private readonly IDistributedCache _cache;
    private readonly ILogger<CacheService> _logger;
    private readonly CacheOptions _options;

    public CacheService(ILogger<CacheService> logger,
                        IOptions<CacheOptions> options,
                        IDistributedCache cache)
    {
        _cache = cache;
        _logger = logger;
        _options = options.Value;
    }

    public async Task<T> GetOrDefault<T>(string key, T defaultVal)
    {

        var bytesResult = await GetAsync(key);

        if (bytesResult?.Length > 0)
        {
            using StreamReader reader = new(new MemoryStream(bytesResult));
            using JsonTextReader jsonReader = new(reader);
            JsonSerializer ser = new();
            ser.TypeNameHandling = TypeNameHandling.All;
            ser.StringEscapeHandling = StringEscapeHandling.EscapeNonAscii;

            var result = ser.Deserialize<T>(jsonReader);
            if (result != null) { return result; }
        }

        return defaultVal;

    }

    public async Task<T> CreateAndSetAsync<T>(string key, Func<Task<T>> createAsync)
    {
        T thing;
        try
        {
            thing = await createAsync();

            await RemoveAsync(key);

            JsonSerializerSettings serializerSettings = new()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.All,
                StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
            };

            var json = JsonConvert.SerializeObject(thing, serializerSettings);

            await SetAsync(key, Encoding.ASCII.GetBytes(json), GetCacheExpirationOptions());

            _logger.LogTrace(string.Format("{0} - Item with key {1} added to cache.", nameof(CreateAndSetAsync), key));

        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred at {nameof(CreateAndSetAsync)}.", e);
            throw;
        }

        return thing;
    }

    private DistributedCacheEntryOptions GetCacheExpirationOptions()
    {
        return new DistributedCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromMinutes(_options.SlidingExpirationToNowInMinutes)
        };
    }


    private async Task RemoveAsync(string key)
    {
        await Locker.WaitAsync();
        try
        {
            await _cache.RemoveAsync(key);
        }
        finally
        {
            Locker.Release();
        }
    }
    private async Task<byte[]?> GetAsync(string key)
    {
        await Locker.WaitAsync();
        try
        {
            var bytesResult = await _cache.GetAsync(key);
            return bytesResult;
        }
        finally
        {
            Locker.Release();
        }
    }

    private async Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options)
    {
        await Locker.WaitAsync();
        try
        {
            await _cache.SetAsync(key, value, options);
        }
        finally
        {
            Locker.Release();
        }
    }

}
