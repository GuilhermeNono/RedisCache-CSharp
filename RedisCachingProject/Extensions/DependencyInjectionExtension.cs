using RedisCachingProject.Abstraction;
using RedisCachingProject.Authentication;
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
        services.AddTransient<AuthenticationService>();

        services.AddScoped<IDbContext, DbContext>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IResponseCacheService, ResponseCacheService>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddScoped<IDeleteCache, DeleteCache>();
        services.AddScoped<IGetCache, GetCache>();
        
        return services;
    }
}