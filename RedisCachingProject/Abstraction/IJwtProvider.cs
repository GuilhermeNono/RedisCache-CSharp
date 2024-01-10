using RedisCachingProject.Database.Model;

namespace RedisCachingProject.Abstraction;

public interface IJwtProvider
{
    public string Generate(Person person);
}