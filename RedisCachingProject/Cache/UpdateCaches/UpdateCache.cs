using Microsoft.AspNetCore.Mvc;
using RedisCachingProject.Cache.Abstraction;
using RedisCachingProject.Cache.DeleteCaches;
using RedisCachingProject.Cache.GetCaches;
using RedisCachingProject.Services;

namespace RedisCachingProject.Cache.UpdateCaches;

public class UpdateCache : Cacheable, IUpdateCache
{
    public int TimeToLive { get; set; } = 30;
    private readonly DeleteCache _deleteCache;
    private readonly GetCache _getCache;

    public override ContentResult ContentResult => new()
    {
        StatusCode = 200
    };
    
    public IActionResult UpdateContextResult(HttpRequest request)
    {
        ContentResult.Content = CachedResponse;
        ContentResult.ContentType = request.ContentType;
        return ContentResult;
    }
    
    public UpdateCache(IResponseCacheService cacheService) : base(cacheService)
    {
        _getCache = new GetCache(cacheService);
        _deleteCache = new DeleteCache(cacheService);
    }

    public override async Task ExecuteCacheResponse(IActionResult result, IResponseCacheService cacheService, string cacheKey)
    {
        await _deleteCache.ExecuteCacheResponse(result, cacheService, cacheKey);
    }

}