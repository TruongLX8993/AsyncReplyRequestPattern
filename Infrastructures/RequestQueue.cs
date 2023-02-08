using System.Text;
using System.Text.Json;
using Domain;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
namespace Infrastructures;

public class RequestQueue : IRequestQueue
{
    private readonly IModel _model;

    public RequestQueue(IConfiguration configuration)
    {
        var uri = configuration["QueueUri"]!;
        var queueName = configuration["QueueName"]!;

        var factory = new ConnectionFactory()
        {
            Uri = new Uri(uri)
        };
        var connection = factory.CreateConnection();
        _model = connection.CreateModel();
        _model.QueueDeclare(queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

    }
    public void Enqueue(Request request)
    {
        _model.BasicPublish(exchange: string.Empty,
            routingKey: "hello",
            basicProperties: null,
            body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request)));
    }
    public void Dispose()
    {
        _model.Dispose();
    }
}