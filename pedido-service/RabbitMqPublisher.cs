namespace pedido_service
{
    using RabbitMQ.Client;
    using System.Text;
    using Newtonsoft.Json;
    using pedido_service.Events; // seu PedidoCriadoEvent

    public static class RabbitMqPublisher
    {
        public static void PublicarPedidoCriado(PedidoCriadoEvent evento)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "pedidos",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var json = JsonConvert.SerializeObject(evento);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "",
                                 routingKey: "pedidos",
                                 basicProperties: null,
                                 body: body);
        }
    }

}
