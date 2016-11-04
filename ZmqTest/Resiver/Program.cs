using System;
using ZeroMQ;

namespace Resiver
{
    class Program
    {
        static void Main(string[] args)
        {
            // Socket to talk to server
            using (var context = new ZContext())
            using (var subscriber = new ZSocket(context, ZSocketType.SUB))
            {
                subscriber.Connect("tcp://127.0.0.1:5555");
                subscriber.Subscribe("Sensor");
                while (true)
                {
                    using (var replyMessage = subscriber.ReceiveMessage())
                    {
                        string messageNameRe = replyMessage[0].ReadString();
                        var messagevalue = replyMessage[1].ReadString();

                        Console.WriteLine("messageName = "+ messageNameRe);
                        Console.WriteLine("messagevalue = " + messagevalue);
                    }

                }

            }
        }
    }
    
}
