using Microsoft.AspNetCore.Mvc;
using RedisCachingProject.Services;

namespace RedisCachingProject.Cache.Abstraction;

public abstract class Cacheable : ICacheable
{
    private readonly IResponseCacheService _cacheService;
    private string? CacheKey { get; set; }
    public string? CachedResponse { get; set; }
    public bool IsGeneratedKeyValid() => !string.IsNullOrEmpty(CachedResponse);
    
    public abstract ContentResult ContentResult { get;}

    protected Cacheable(IResponseCacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public string GenerateCacheKey(HttpRequest request)
    {
        return CacheKey = CacheUtil.GenerateCacheKey(request);
    }
    
    public async Task<string?> GetCachedResponse(string key)
    {
        return CachedResponse = await _cacheService.GetCachedStringResponseAsync(key);
    }
    
    
    
    public abstract Task ExecuteCacheResponse(IActionResult result, IResponseCacheService cacheService, string cacheKey);
}