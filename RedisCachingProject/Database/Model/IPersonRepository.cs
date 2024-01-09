using RedisCachingProject.Abstraction;

namespace RedisCachingProject.Database.Model;

public interface IPersonRepository: IRepository<Person, long>
{
    
}