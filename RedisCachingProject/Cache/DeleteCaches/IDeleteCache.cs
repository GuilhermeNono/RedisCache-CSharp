using Microsoft.AspNetCore.Mvc;
using RedisCachingProject.Cache.Abstraction;

namespace RedisCachingProject.Cache.DeleteCaches;

public interface IDeleteCache: ICacheable
{
    public IActionResult UpdateContextResult();
}