namespace RedisCachingProject.Abstraction;

public interface IRepository<TEntity, TId> where TEntity: class
{
    Task<IEnumerable<TEntity>> Find();
    Task<TEntity?> FindById(TId id);
    Task<bool> Update(TEntity entity);
    Task<bool> Delete(TEntity entity);
    Task<int> Add(TEntity entity);
}