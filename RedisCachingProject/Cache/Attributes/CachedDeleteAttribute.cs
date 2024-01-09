using Microsoft.AspNetCore.Mvc.Filters;
using RedisCachingProject.Cache.DeleteCaches;
using RedisCachingProject.Services;

namespace RedisCachingProject.Cache.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class CachedDeleteAttribute : Attribute, IAsyncActionFilter
{
    private string? RouteCachedList { get; set; }

    public CachedDeleteAttribute()
    {
    }

    public CachedDeleteAttribute(string routeCachedList)
    {
        RouteCachedList = routeCachedList;
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

        var deleteCache = new DeleteCache(cacheService);

        var cacheKey = string.Empty;

        cacheKey = string.IsNullOrEmpty(RouteCachedList)
            ? deleteCache.GenerateCacheKey(context.HttpContext.Request)
            : RouteCachedList;

        await deleteCache.GetCachedResponse(cacheKey);

        context.HttpContext.Response.Headers.Add(new("cache-control", "private, no-cache"));

        if (deleteCache.IsGeneratedKeyValid())
        {
            var executedContext = await next();

            await deleteCache.ExecuteCacheResponse(executedContext.Result, cacheService, cacheKey);
            return;
        }

        await next();
    }
}