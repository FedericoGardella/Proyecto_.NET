namespace GestionCitas.Messaging

{
    using RabbitMQ.Client;
    using System;
    using System.Text;

    public class ReminderPublisher
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public ReminderPublisher()
        {
            _factory = new ConnectionFactory
            {
                HostName = "rabbitmq", // Nombre del servicio en `docker-compose`
                UserName = "dotnetlab",
                Password = "P455w0rd_net"
            };

            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "recordatorios",
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void Publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                  routingKey: "recordatorios",
                                  basicProperties: null,
                                  body: body);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
