using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
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

    public async Task<TEntity?> FindByName(string name)
    {
        var sql = new StringBuilder();

        sql.Append(" Select * ");
        sql.Append("  From Person ");
        sql.Append(" Where FirstName like @Name ");
        
        return await _context.Connection.QuerySingleAsync<TEntity>(sql.ToString(), new {name});
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