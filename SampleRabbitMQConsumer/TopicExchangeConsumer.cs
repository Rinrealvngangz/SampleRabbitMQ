using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SampleRabbitMQ
{
    public class TopicExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("sample-topic-exchange",ExchangeType.Topic);
            channel.QueueDeclare("sample-topic-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments:null);
            channel.QueueBind("sample-topic-queue","sample-topic-exchange","account.*");
            channel.BasicQos(0,10,false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var result = Encoding.UTF8.GetString(body);
                Console.WriteLine(result);
            };
            channel.BasicConsume("sample-topic-queue", true, consumer);
            Console.ReadLine();
        } 
    }
}