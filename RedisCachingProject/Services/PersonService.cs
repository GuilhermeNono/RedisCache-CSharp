using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RedisCachingProject.Database.Model;
using RedisCachingProject.DTO;
using StackExchange.Redis;

namespace RedisCachingProject.Services;

public class PersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IDistributedCache _distributedCache;

    public PersonService(IPersonRepository personRepository, IDistributedCache distributedCache)
    {
        _personRepository = personRepository;
        _distributedCache = distributedCache;
    }

    public async Task<IEnumerable<PersonResult>> Get()
    {
        var persons = await _personRepository.Find();
        
        return persons.Select(PersonResult.ToResult);
    }

    public async Task<long> CreatePerson(PersonUpdateResult entity, string cacheKey)
    {
        var person = new Person()
        {
            Age = entity.Age,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
        };
        
        var personId = await _personRepository.Add(person);
        
        await _distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(person));

        return personId;
    }

    public async Task<PersonResult> GetById(long id)
    {
        var person = await _personRepository.FindById(id) ??
                     throw new Exception("Usuario inexistente!");
        return PersonResult.ToResult(person);
    }

    public async Task<bool> Update(long id, PersonUpdateResult entity, string cacheKey)
    {

        string? cachedPerson = await _distributedCache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedPerson))
            await _distributedCache.RemoveAsync(cacheKey);

        var person = await _personRepository.FindById(id) ??
                     throw new Exception("Usuario Inexistente.");

        person.Age = entity.Age;
        person.FirstName = entity.FirstName;
        person.LastName = entity.LastName;

        var isUpdated = await _personRepository.Update(person);

        await _distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(person));
        
        return isUpdated;
    }

    public async Task Delete(long id)
    {
        var person = await _personRepository.FindById(id) ??
                     throw new Exception("Usuario Inexistente.");
        
        await _personRepository.Delete(person);
    }
}