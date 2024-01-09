namespace RedisCachingProject.Extensions;

public static class RedisExtension
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = configuration.GetConnectionString("Redis");
        });
        
        return services;
    }
}