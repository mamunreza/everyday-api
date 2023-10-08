using System.Text;
using System.Text.Json;
using Everyday.Messages;
using RabbitMQ.Client;

namespace Everyday.Services;

public interface IUserService
{
    Task CreateUserAsync(UserCreated userCreated);
}

public class UserService : IUserService
{
    public async Task CreateUserAsync(UserCreated userCreated)
    {
        await Process(userCreated);
    }

    public async Task Process(UserCreated userCreated)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        using var connection = factory.CreateConnection();

        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "everyday", type: ExchangeType.Fanout);

        var message = JsonSerializer.Serialize(userCreated);

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "everyday", "", null, body);

        Console.WriteLine($"Send message: {message}");

        await Task.CompletedTask;
    }
}