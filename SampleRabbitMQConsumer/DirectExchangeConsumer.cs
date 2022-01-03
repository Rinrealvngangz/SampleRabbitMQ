using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace SampleRabbitMQ

{
    public class DirectExchangeConsumer
    {
         public static void Consume(IModel channel)
                {
                    channel.ExchangeDeclare("sample-direct-exchange",ExchangeType.Direct);
                    channel.QueueDeclare("sample-direct-queue",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments:null);
                    channel.QueueBind("sample-direct-queue","sample-direct-exchange","account.init");
                 channel.BasicQos(0,10,false);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (ch, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var result = Encoding.UTF8.GetString(body);
                        Console.WriteLine(result);
                    };
                    channel.BasicConsume("sample-direct-queue", true, consumer);
                    Console.ReadLine();
                }
    }
}