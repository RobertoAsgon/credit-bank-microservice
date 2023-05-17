using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

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

            channel.QueueDeclare("cadastroCliente", exclusive: false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "cadastro", routingKey: "cliente", body: body);
        }
    }
}
