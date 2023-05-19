using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using Credit.Domain.Entities;

namespace Credit.Infrastructure.RabbitMQ
{
    public class RabitMQProducer : IRabitMQProducer
    {
        public void SendClientMessage<Cliente>(Cliente message)
        {

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };


            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "cadastroCliente",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "cliente", body: body);
        }
    }
}
