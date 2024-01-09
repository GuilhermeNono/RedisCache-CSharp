using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace RedisCachingProject.Abstraction;

public abstract class Repository<TEntity, TId>: IRepository<TEntity, TId> where TEntity : class
{

    private readonly IDbContext _context;
    
    protected Repository(IDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TEntity>> Find()
    {
        return await _context.Connection.GetAllAsync<TEntity>();
    }

    public async Task<TEntity?> FindById(TId id)
    {
        return await _context.Connection.GetAsync<TEntity>(id);
    }

    public async Task<bool> Update(TEntity entity)
    {
        return await _context.Connection.UpdateAsync(entity);
    }

    public async Task<bool> Delete(TEntity entity)
    {
        return await _context.Connection.DeleteAsync(entity);
    }

    public async Task<int> Add(TEntity entity)
    {
        return await _context.Connection.InsertAsync(entity);
    }
}