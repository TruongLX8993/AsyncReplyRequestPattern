
using Domain.Entities;
namespace Domain.Repository;

public interface IResponseRepository : IRepository<string, Response>
{
    Task<Response?> GetByRequestId(string requestId);
}