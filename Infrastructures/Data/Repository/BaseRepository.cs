using Domain.Repository;
using ISession=NHibernate.ISession;
namespace Api.Infrastructures.Data.Repository;

public class BaseRepository<TKey, TEntity> : IRepository<TKey, TEntity>
{
    protected readonly ISession Session;
    public BaseRepository(ISession session)
    {
        Session = session;
    }
    public async Task<TKey> Add(TEntity entity)
    {
        return (TKey)await Session.SaveAsync(entity);
    }
    public async Task<TEntity?> GetById(TKey id)
    {
        return (TEntity)await Session.GetAsync(typeof(TEntity), id);
    }
}