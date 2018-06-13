using RabbitMQ.Client;
using System;
using System.Text;

namespace TransactionSend
{
    class Program
    {
        public static readonly string ExchangeName = "Ex_confirm";
        public static readonly string QueueName = "Qu_confirm";
        public static readonly string RouteKey = "confirm";

        /// <summary>
        /// 事务控制方式  事务控制的方式效率要低于comfim方式
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "119.29.225.20",
                UserName="dirkwang",
                Password="wangjun1234"
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(ExchangeName, "direct", false, false, null);
                    channel.QueueDeclare(QueueName, false, false, false, null);
                    channel.QueueBind(QueueName, ExchangeName, RouteKey, null);
                    try
                    {
                        channel.TxSelect();
                        for (int i = 0; i < 100; i++)
                        {
                            if (i == 10)
                            {
                                throw new Exception("发送消息失败");
                            }
                            string msg = "这是第" + i + "条消息";
                            var body = Encoding.UTF8.GetBytes(msg);
                            channel.BasicPublish(ExchangeName, RouteKey, null, body);
                        }
                        channel.TxCommit();
                        Console.WriteLine("消息发送成功。");
                    }
                    catch (Exception ex)
                    {
                        channel.TxRollback();
                        Console.WriteLine(ex.Message);
                    }
                    
                }
            }

            Console.ReadKey();
        }
    }
}
