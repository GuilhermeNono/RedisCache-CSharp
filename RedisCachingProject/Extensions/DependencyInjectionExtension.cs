using RedisCachingProject.Abstraction;
using RedisCachingProject.Cache.Abstraction;
using RedisCachingProject.Cache.DeleteCaches;
using RedisCachingProject.Cache.GetCaches;
using RedisCachingProject.Database.Model;
using RedisCachingProject.Services;

namespace RedisCachingProject.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddIOC(this IServiceCollection services)
    {
        services.AddTransient<PersonService>();

        services.AddScoped<IDbContext, DbContext>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IResponseCacheService, ResponseCacheService>();

        services.AddScoped<IDeleteCache, DeleteCache>();
        services.AddScoped<IGetCache, GetCache>();
        
        return services;
    }
}