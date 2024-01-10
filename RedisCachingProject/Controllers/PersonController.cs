using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedisCachingProject.Cache;
using RedisCachingProject.Cache.Attributes;
using RedisCachingProject.DTO;
using RedisCachingProject.Services;

namespace RedisCachingProject.Controllers;

[Route("persons")]
[ApiController]

public class PersonController: ControllerBase
{

    private readonly PersonService _personService;

    public PersonController(PersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    [Cached(60)]
    public async Task<ActionResult<IEnumerable<PersonResult>>> GetAllPerson()
    {
        return Ok(await _personService.Get());
    }
    
    [Authorize]
    [HttpGet("{id}")]
    [Cached(90)]
    public async Task<ActionResult<PersonResult>> GetPerson(long id)
    {
        return Ok(await _personService.GetById(id));
    }
    
    [HttpPost]
    [CachedDelete]
    public async Task<ActionResult<long>> PostPerson([FromBody] PersonUpdateResult entity)
    {
        var personId = await _personService.CreatePerson(entity, CacheUtil.GenerateCacheKey(Request));
        return Created($"/persons/{personId}", personId);
    }
    
    [HttpPut("{id}")]
    [CachedDelete("/persons")]
    [CachedDelete]
    public async Task<ActionResult<bool>> UpdatePerson(long id, [FromBody]PersonUpdateResult request)
    {
        return Ok(await _personService.Update(id, request, CacheUtil.GenerateCacheKey(Request)));
    }
    
    [HttpDelete("{id}")]
    [CachedDelete]
    public async Task<NoContentResult> DeletePerson(long id)
    {
        await _personService.Delete(id);
        return NoContent();
    }

    [HttpGet("image")]
    [Cached(60)]
    public async Task<ActionResult> GetProfilePicture()
    {
        using var client = new HttpClient();
        var bytes = await client.GetByteArrayAsync("https://buffer.com/cdn-cgi/image/w=1000,fit=contain,q=90,f=auto/library/content/images/size/w1200/2023/10/free-images.jpg");
        // var teste = "image/jpeg;base64," + Convert.ToBase64String(bytes);
        return new FileContentResult(bytes, "image/jpeg");
    }
    
}