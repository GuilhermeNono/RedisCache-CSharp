using RedisCachingProject.Abstraction;
using RedisCachingProject.Database.Model;

namespace RedisCachingProject.Services;

public class AuthenticationService
{
    private readonly IPersonRepository _personRepository;
    private readonly IJwtProvider _jwtProvider;

    public AuthenticationService(IPersonRepository personRepository, IJwtProvider jwtProvider)
    {
        _personRepository = personRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> Login(string name)
    {
        var person = await _personRepository.FindByName(name) ??
                     throw new Exception("Esse usuario não existe");

        var token = _jwtProvider.Generate(person);
        return token;
    }
}