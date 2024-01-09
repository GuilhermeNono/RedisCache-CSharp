using System.Collections;
using Microsoft.AspNetCore.Mvc;
using RedisCachingProject.Cache.Abstraction;
using RedisCachingProject.Services;

namespace RedisCachingProject.Cache.GetCaches;

public class GetCache : Cacheable, IGetCache
{
    public int TimeToLive { get; set; } = 30;

    public override ContentResult ContentResult => new()
    {
        StatusCode = 200
    };

    public GetCache(IResponseCacheService cacheService) : base(cacheService)
    {
    }

    public IActionResult UpdateContextResult(HttpRequest request)
    {
        ContentResult.Content = CachedResponse;
        ContentResult.ContentType = request.ContentType;
        return ContentResult;
    }


    public override async Task ExecuteCacheResponse(IActionResult result, IResponseCacheService cacheService,
        string cacheKey)
    {
        switch (result)
        {
            case OkObjectResult objectResult:
            {
                if(objectResult.Value is IEnumerable)
                    await cacheService.CacheStringResponseAsync(cacheKey, objectResult.Value,
                        TimeSpan.FromSeconds(TimeToLive));
                else
                    await cacheService.CacheStringResponseAsync(cacheKey, objectResult.Value,
                        TimeSpan.FromSeconds(TimeToLive));
                break;
            }
            case FileContentResult fileContentResult:
                await cacheService.CacheByteResponseAsync(cacheKey, fileContentResult.FileContents,
                    TimeSpan.FromSeconds(TimeToLive));
                break;
        }
    }
}