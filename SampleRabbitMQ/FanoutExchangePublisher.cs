using System;
using System.Collections.Generic;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;
using System.Threading;

namespace SampleRabbitMQ
{
    public class FanoutExchangePublisher
    {
        public static void Publisher(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000}
            };
            channel.ExchangeDeclare("sample-fanout-exchange", ExchangeType.Fanout,arguments: ttl);
            var count = 0;
            while (true)
            {
                var messager = new {Name = "Producer", Text = $"Hello: count {count}"};
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messager));
                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object>{{"account","new"}};
                channel.BasicPublish("sample-fanout-exchange","account.new",properties,body);
                count++;
                Thread.Sleep(1000);
            }
            
        }
    }
}