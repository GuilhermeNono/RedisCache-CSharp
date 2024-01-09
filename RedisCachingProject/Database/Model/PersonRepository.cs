using RedisCachingProject.Abstraction;

namespace RedisCachingProject.Database.Model;

public class PersonRepository: Repository<Person, long>, IPersonRepository
{
    public PersonRepository(IDbContext context) : base(context)
    {
    }
}