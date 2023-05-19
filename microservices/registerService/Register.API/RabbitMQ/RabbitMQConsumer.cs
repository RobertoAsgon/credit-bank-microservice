using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Register.API.Models;
using Newtonsoft.Json;
using System.Diagnostics;

public class RabbitMQConsumer : BackgroundService
{
    private readonly string queueName;
    private readonly string routingKey;
    private IConnection connection;
    private IModel channel;

    public RabbitMQConsumer(string queueName, string routingKey)
    {
        this.queueName = queueName;
        this.routingKey = routingKey;
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

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Debug.WriteLine("message: " + message);

            if (routingKey == "cliente")
            {
                var cliente = JsonConvert.DeserializeObject<Cliente>(message);

                Console.WriteLine("Cliente cadastrado: " + message);
            }

        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        channel?.Close();
        connection?.Close();

        await base.StopAsync(stoppingToken);
    }
}
