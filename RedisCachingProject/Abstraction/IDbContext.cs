using System.Data;

namespace RedisCachingProject.Abstraction;

public interface IDbContext
{
    IDbConnection Connection { get; }
}