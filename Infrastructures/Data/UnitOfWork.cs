using Domain.Repository;
using NHibernate;
using ISession=NHibernate.ISession;
namespace Api.Infrastructures.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ISession _session;
    private ITransaction _transaction;
    public UnitOfWork(ISession session)
    {
        _session = session;
    }
    public void Begin()
    {
        _transaction = _session.BeginTransaction();
    }
    public async Task Commit()
    {
        await _transaction.CommitAsync();
    }
    public async Task Rollback()
    {
        await _transaction.RollbackAsync();
    }
}