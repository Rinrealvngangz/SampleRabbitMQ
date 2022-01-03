using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SampleRabbitMQ
{
    public class HeaderExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("sample-header-exchange",ExchangeType.Headers);
            channel.QueueDeclare("sample-header-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments:null);
            var header = new Dictionary<string, object> {{"account", "new"}};
            channel.QueueBind("sample-header-queue","sample-header-exchange",String.Empty,header);
            channel.BasicQos(0,10,false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var result = Encoding.UTF8.GetString(body);
                Console.WriteLine(result);
            };
            channel.BasicConsume("sample-header-queue", true, consumer);
            Console.ReadLine();
        } 
    }
}