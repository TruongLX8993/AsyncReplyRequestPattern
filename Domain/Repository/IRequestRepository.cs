using Domain.Entities;
namespace Domain.Repository;

public interface IRequestRepository : IRepository<string, Request>
{
    Task<RequestStatus> GetStatus(string requestId);
}