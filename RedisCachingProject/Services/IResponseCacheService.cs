namespace RedisCachingProject.Services;

public interface IResponseCacheService
{
    Task CacheStringResponseAsync(string cacheKey, object response, TimeSpan timeLive);
    Task<string?> GetCachedStringResponseAsync(string? cacheKey);
    Task CacheByteResponseAsync(string cacheKey, object response, TimeSpan timeLive);
    Task RemoveCachedResponseAsync(string cacheKey);
}