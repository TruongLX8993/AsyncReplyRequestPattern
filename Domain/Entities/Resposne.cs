namespace Domain.Entities;

public class Response : TEntity<string>
{
    public virtual string Body { get; set; }
    public virtual string RequestId { get; set; }
}