using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Register.API.Models;
using Newtonsoft.Json;
using Register.API.Data;

public class RabbitMQConsumer : BackgroundService
{
    private readonly string queueName;
    private readonly string exchangeName;
    private readonly string routingKey;
    private IConnection connection;
    private IModel channel;
    private readonly DbContextClass dbContext;
    public RabbitMQConsumer(string queueName, string exchangeName, string routingKey, DbContextClass dbContext)
    {
        this.queueName = queueName;
        this.exchangeName = exchangeName;
        this.routingKey = routingKey;
        this.dbContext = dbContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        connection = factory.CreateConnection();
        channel = connection.CreateModel();

        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);


            if (routingKey == "cliente")
            {
                var cliente = JsonConvert.DeserializeObject<Cliente>(message);

                await dbContext.Cliente.AddAsync(cliente);
                await dbContext.SaveChangesAsync();
                Console.WriteLine("Cliente cadastrado: " + message);
            }

        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        await Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        channel?.Close();
        connection?.Close();

        await base.StopAsync(stoppingToken);
    }
}
