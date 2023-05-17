

namespace Credit.Infrastructure.RabbitMQ
{
    public interface IRabitMQProducer
    {
        public void SendClientMessage<Cliente>(Cliente message);
    }
}
