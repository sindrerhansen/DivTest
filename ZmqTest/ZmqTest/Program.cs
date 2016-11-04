using System;


namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventPub = new EventPublisher();
            eventPub.Start();
            Console.ReadLine();
            eventPub.Stop();
                       
        }
    }
}
