using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SampleRabbitMQ
{
    public class QueueConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.QueueDeclare("sample-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments:null);
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var result = Encoding.UTF8.GetString(body);
                Console.WriteLine(result);
            };
            channel.BasicConsume("sample-queue", true, consumer);
            Console.ReadLine();
        }
    }
}