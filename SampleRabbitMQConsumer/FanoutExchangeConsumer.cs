using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SampleRabbitMQ
{
    public class FanoutExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("sample-fanout-exchange",ExchangeType.Fanout);
            channel.QueueDeclare("sample-fanout-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments:null);
            channel.QueueBind("sample-fanout-queue","sample-fanout-exchange",String.Empty);
            channel.BasicQos(0,10,false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var result = Encoding.UTF8.GetString(body);
                Console.WriteLine(result);
            };
            channel.BasicConsume("sample-fanout-queue", true, consumer);
            Console.ReadLine();
        } 
    }
}