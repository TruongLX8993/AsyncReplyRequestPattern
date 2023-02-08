namespace Domain.Entities;

public class Request : TEntity<string>
{
    public virtual string Body { get; set; }
    public virtual RequestStatus Status { get; set; }
}