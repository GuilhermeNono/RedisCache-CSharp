using Microsoft.AspNetCore.Mvc.Filters;
using RedisCachingProject.Cache.GetCaches;
using RedisCachingProject.Services;

namespace RedisCachingProject.Cache.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class CachedAttribute : Attribute, IAsyncActionFilter
{
    private readonly int _timeToLiveSecond;
    
    public CachedAttribute(int timeToLiveSecond)
    {
        _timeToLiveSecond = timeToLiveSecond;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        var cacheSettings = context.HttpContext.RequestServices.GetService<RedisCacheSettings>();

        
        if (!cacheSettings!.Enabled)
        {
            await next();
            return;
        }

        var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
        var getCache = new GetCache(cacheService)
        {
            TimeToLive = _timeToLiveSecond
        };

        var cacheKey = getCache.GenerateCacheKey(context.HttpContext.Request);
        await getCache.GetCachedResponse(cacheKey);

        context.HttpContext.Response.Headers.Add(new("cache-control", $"max-age={_timeToLiveSecond}, private"));
        if (getCache.IsGeneratedKeyValid())
        {
            context.Result = getCache.UpdateContextResult(context.HttpContext.Request);
            return;
        }

        var executedContext = await next();

        await getCache.ExecuteCacheResponse(executedContext.Result, cacheService, cacheKey);
    }
}