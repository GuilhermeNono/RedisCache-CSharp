using Microsoft.AspNetCore.Mvc;
using RedisCachingProject.Services;

namespace RedisCachingProject.Cache.Abstraction;

public interface ICacheable
{
    public string GenerateCacheKey(HttpRequest request);
    public Task<string?> GetCachedResponse(string key);
    public Task ExecuteCacheResponse(IActionResult result, IResponseCacheService cacheService, string cacheKey);
    public bool IsGeneratedKeyValid();
    protected ContentResult ContentResult{ get; }
    public string? CachedResponse { get; set; }
}