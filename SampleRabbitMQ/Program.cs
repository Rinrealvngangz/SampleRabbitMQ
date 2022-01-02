using System;
using System.Text;
using RabbitMQ.Client;
using Newtonsoft.Json;
namespace SampleRabbitMQ
{
    class Program
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
         //  QueuePublisher.Publisher(channel);
         DirectExchangePublisher.Publisher(channel);
        }
    }
}