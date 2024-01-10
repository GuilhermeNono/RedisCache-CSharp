using Microsoft.AspNetCore.Mvc;
using RedisCachingProject.Services;

namespace RedisCachingProject.Controllers;

[Route("auth")]
[ApiController]
public class AuthenticationController: ControllerBase
{
    private readonly AuthenticationService _authenticationService;

    public AuthenticationController(AuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> LoginUser(string name)
    {
        return await _authenticationService.Login(name);
    }
}