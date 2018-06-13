using RabbitMQ.Client;
using System;
using System.Text;

namespace PublishAndSubscirbeSend
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
                HostName = "119.29.225.20",
                UserName = "dirkwang",
                Password = "wangjun1234"
            };
            using (var connction = factory.CreateConnection())
            {
                using (var channel = connction.CreateModel())
                {
                    channel.ExchangeDeclare(ExchangeName, "direct", false, false, null);
                    channel.QueueDeclare(QueueName, false, false, false, null);
                    channel.QueueBind(QueueName, ExchangeName, RouteKey, null);
                    for (int i = 0; i < 50; i++)
                    {
                        string message = "hello publish/subscirbe"+i;
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(ExchangeName, RouteKey, false, null, body);
                        Console.WriteLine("成功发送消息:{0}", message);
                    }
                   
                }
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
