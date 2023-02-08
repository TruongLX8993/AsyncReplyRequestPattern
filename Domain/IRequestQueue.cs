using Domain.Entities;
namespace Domain;

public interface IRequestQueue : IDisposable
{
    void Enqueue(Request request);
}