namespace Domain.Repository;

public interface IUnitOfWork
{
    void Begin();
    Task Commit();
    Task Rollback();
}