using System.Text;

namespace RedisCachingProject.Cache;

public static class CacheUtil
{
    public static string GenerateCacheKey(HttpRequest request)
    {
        var keyBuilder = new StringBuilder();

        keyBuilder.Append($"{request.Path}");

        foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
        {
            keyBuilder.Append($"|{key} - {value}");
        }

        return keyBuilder.ToString();
    }
    
    public static string GenerateCacheKey(string pathKey)
    {
        var keyBuilder = new StringBuilder();

        keyBuilder.Append($"{pathKey}");

        return keyBuilder.ToString();
    }
}