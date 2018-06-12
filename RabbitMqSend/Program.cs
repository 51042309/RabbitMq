using System;

namespace RabbitMqSend
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleSend send = new SimpleSend();
            send.Send();
            Console.ReadKey();
        }
    }
}
