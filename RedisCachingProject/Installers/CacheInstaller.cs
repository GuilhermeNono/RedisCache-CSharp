using RedisCachingProject.Cache;
using RedisCachingProject.Services;

namespace RedisCachingProject.Installers;

public static class CacheInstaller
{
    public static void InstallServices(this IServiceCollection services, IConfiguration configuration)
    {
        var redisCacheSettings = new RedisCacheSettings();
        configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);
        services.AddSingleton(redisCacheSettings);
        
        if(!redisCacheSettings.Enabled)
            return;

        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = redisCacheSettings.ConnectionString;
        });
        services.AddSingleton<IResponseCacheService, ResponseCacheService>();
    }
}