using Api.Infrastructures.Data.Repository;
using Domain.Entities;
using Domain.Repository;
using NHibernate.Linq;
using ISession=NHibernate.ISession;
namespace Infrastructures.Data.Repository;

public class RequestRepository : BaseRepository<string, Request>, IRequestRepository
{

    public RequestRepository(ISession session) : base(session)
    {

    }
    public async Task<RequestStatus> GetStatus(string requestId)
    {
        return await Session.Query<Request>()
            .Where(x => x.Id == requestId)
            .Select(x => x.Status)
            .FirstOrDefaultAsync();
    }
}