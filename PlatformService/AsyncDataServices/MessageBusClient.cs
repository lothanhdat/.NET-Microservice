using System.Text;
using System.Text.Json;
using PlatformService.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly ConnectionFactory _factory;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            _factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };
            try
            {
                //_channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                //_connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine("--> Connected to MessageBus");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not connect to the Message Bus: {ex.Message}");
            }
        }

        public async Task PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            //await channel.QueueDeclareAsync(queue: "hello",
            //    durable: false,
            //    exclusive: false,
            //    autoDelete: false,
            //    arguments: null);

            var message = JsonSerializer.Serialize(platformPublishedDto);

            if (connection.IsOpen)
            {
                Console.WriteLine("--> RabbitMQ Connection Open, sending message...");
                await SendMessage(message, channel);
            }
            else
            {
                Console.WriteLine("--> RabbitMQ connectionis closed, not sending");
            }
            connection.ConnectionShutdownAsync += RabbitMQ_ConnectionShutdown;
        }

        private async Task SendMessage(string message, IChannel channel)
        {
            var body = Encoding.UTF8.GetBytes(message);
            await channel.ExchangeDeclareAsync(exchange: "trigger", type: ExchangeType.Fanout);
            await channel.BasicPublishAsync(exchange: "trigger",
                            routingKey: "",
                            body: body);
            Console.WriteLine($"--> We have sent {message}");
        }

        private async Task RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            await Task.Run(() => { Console.WriteLine("--> RabbitMQ Connection Shutdown"); });
        }
    }
}