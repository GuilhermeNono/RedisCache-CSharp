using Microsoft.AspNetCore.Mvc;
using RedisCachingProject.Cache.Abstraction;

namespace RedisCachingProject.Cache.UpdateCaches;

public interface IUpdateCache : ICacheable
{
    public IActionResult UpdateContextResult(HttpRequest request);
}