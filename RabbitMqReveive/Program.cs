using System;

namespace RabbitMqReveive
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleReveive reveice = new SimpleReveive();
            reveice.Receive();
            Console.ReadKey();
        }
    }
}
