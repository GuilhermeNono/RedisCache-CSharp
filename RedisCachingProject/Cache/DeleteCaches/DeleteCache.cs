using Microsoft.AspNetCore.Mvc;
using RedisCachingProject.Cache.Abstraction;
using RedisCachingProject.Services;

namespace RedisCachingProject.Cache.DeleteCaches;

public class DeleteCache : Cacheable, IDeleteCache
{
    public DeleteCache(IResponseCacheService cacheService) : base(cacheService)
    {
    }

    public override ContentResult ContentResult => new();

    public override async Task ExecuteCacheResponse(IActionResult result, IResponseCacheService cacheService,
        string cacheKey)
    {
        await cacheService.RemoveCachedResponseAsync(cacheKey);
    }

    public IActionResult UpdateContextResult()
    {
        return ContentResult;
    }
}