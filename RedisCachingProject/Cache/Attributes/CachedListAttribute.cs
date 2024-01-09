using Microsoft.AspNetCore.Mvc.Filters;

namespace RedisCachingProject.Cache.Attributes;

public class CachedListAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        throw new NotImplementedException();
    }
}