using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace RedisCachingProject.Services;

public class ResponseCacheService: IResponseCacheService
{

    private readonly IDistributedCache _distributedCache;

    public ResponseCacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task CacheStringResponseAsync(string cacheKey, object response, TimeSpan timeLive)
    {
        if(response == null)
            return;

        var serializedResponse = JsonSerializer.Serialize(response);

        await _distributedCache.SetStringAsync(cacheKey, serializedResponse, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = timeLive
        });
    }
    
    public async Task CacheByteResponseAsync(string cacheKey, object response, TimeSpan timeLive)
    {
        if(response == null)
            return;

        var byteResponse = (byte[])response;

        await _distributedCache.SetAsync(cacheKey, byteResponse, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = timeLive
        });
    }

    public async Task<string?> GetCachedStringResponseAsync(string? cacheKey)
    {
        return await _distributedCache.GetStringAsync(cacheKey);
    }

    public async Task RemoveCachedResponseAsync(string cacheKey)
    {
        await _distributedCache.RemoveAsync(cacheKey);
    }
}