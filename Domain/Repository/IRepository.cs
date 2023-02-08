namespace Domain.Repository;

public interface IRepository<TKey, TEntity>
{
    Task<TKey> Add(TEntity entity);
    Task<TEntity?> GetById(TKey id);
}