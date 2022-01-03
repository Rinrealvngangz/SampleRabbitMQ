using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
namespace SampleRabbitMQ
{
   static class Program
    {
        static void Main(string[] args)
        {
            var uri = new Uri("amqp://guest:guest@localhost:5672");
            ConnectionFactory factory = new ConnectionFactory
            {
                Uri = uri
            };
            using  IConnection conn = factory.CreateConnection();
            using  var  channel = conn.CreateModel();
          //  QueueConsumer.Consume(channel);
         //   DirectExchangeConsumer.Consume(channel);
         //   TopicExchangeConsumer.Consume(channel);
          //    HeaderExchangeConsumer.Consume(channel); 
            FanoutExchangeConsumer.Consume(channel);
        }
    }
}