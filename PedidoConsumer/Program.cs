using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PedidoConsumer
{
    class Program
    {
        static void Main()
        {
            const int maxRetries = 10;
            int retryCount = 0;
            IConnection? connection = null;

            var factory = new ConnectionFactory()
            {
                HostName = "rabbitmq", // nome do serviço no docker-compose
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            while (connection == null && retryCount < maxRetries)
            {
                try
                {
                    connection = factory.CreateConnection();
                } catch
                {
                    retryCount++;
                    Console.WriteLine($"🔁 Tentativa {retryCount}/{maxRetries}: aguardando RabbitMQ...");
                    Thread.Sleep(3000);
                }
            }

            if (connection == null)
            {
                Console.WriteLine("❌ Não foi possível conectar ao RabbitMQ.");
                return;
            }

            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "pedidos",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Console.WriteLine("📥 Aguardando mensagens de pedidos...");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    var pedido = JsonConvert.DeserializeObject<PedidoCriadoEvent>(message);
                    Console.WriteLine($"✔️ Novo pedido recebido:");
                    Console.WriteLine($"🧾 ID: {pedido?.PedidoId}");
                    Console.WriteLine($"👤 ClienteId: {pedido?.ClienteId}");
                    Console.WriteLine($"🍔 Itens: {pedido?.ItensResumo}");
                    Console.WriteLine($"📅 Data: {pedido?.DataPedido}");
                    Console.WriteLine("--------------------------------------------");
                } catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao deserializar mensagem: {ex.Message}");
                }
            };

            channel.BasicConsume(queue: "pedidos", autoAck: true, consumer: consumer);

            Console.WriteLine("Consumidor pronto. Pressione Ctrl+C para encerrar.");
            // Mantém o processo vivo em Docker e local
            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
