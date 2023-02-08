using Api.Infrastructures.Data.Repository;
using Domain.Entities;
using Domain.Repository;
using NHibernate.Linq;
using ISession=NHibernate.ISession;
namespace Infrastructures.Data.Repository;

public class ResponseRepository : BaseRepository<string, Response>, IResponseRepository
{

    public ResponseRepository(ISession session) : base(session)
    {

    }
    public async Task<Response?> GetByRequestId(string requestId)
    {
        return await Session.Query<Response>()
            .Where(x => x.RequestId == requestId)
            .FirstOrDefaultAsync();
    }
}