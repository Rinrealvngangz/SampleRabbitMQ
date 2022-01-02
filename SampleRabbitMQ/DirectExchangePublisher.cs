using System;
using System.Collections.Generic;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
namespace SampleRabbitMQ
{
    public class DirectExchangePublisher
    {
        public static void Publisher(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000}
            };
            channel.ExchangeDeclare("sample-direct-exchange", ExchangeType.Direct,arguments: ttl);
            var count = 0;
            while (true)
            {
                var messager = new {Name = "Producer", Text = $"Hello: count {count}"};
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messager));
                channel.BasicPublish("sample-direct-exchange","account.init",null,body);
                count++;
                Thread.Sleep(1000);
            }
            
        }
    }
}