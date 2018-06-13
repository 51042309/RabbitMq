using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace PublishAndSubscribeReveice
{
    class Program
    {
        public static readonly string ExchangeName = "log";
        public static readonly string QueueName = "log_queue";
        public static readonly string RouteKey = "error";

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName="119.29.225.20",
                UserName="dirkwang",
                Password="wangjun1234"
            };
            using (var connction = factory.CreateConnection())
            {
                using (var channel = connction.CreateModel())
                {
                    channel.ExchangeDeclare(ExchangeName, "direct", false, false, null);
                    channel.QueueDeclare(QueueName, false, false, false, null);
                    channel.QueueBind(QueueName, ExchangeName, RouteKey, null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) => {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("接收到消息:{0}",message);
                    };
                    channel.BasicConsume(QueueName, false, consumer);
                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
