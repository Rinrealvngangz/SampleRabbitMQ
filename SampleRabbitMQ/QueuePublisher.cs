using System;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;
using System.Threading;

namespace SampleRabbitMQ
{
    public static class QueuePublisher
    {
        public static void Publisher(IModel channel)
        {
            channel.QueueDeclare("sample-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments:null);
            var count = 0;
            while (true)
            {
                var messager = new {Name = "Producer", Text = $"Hello: count {count}"};
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messager));
                channel.BasicPublish("","sample-queue",null,body);
                count++;
                Thread.Sleep(1000);
            }
            
        }
       
    }
}