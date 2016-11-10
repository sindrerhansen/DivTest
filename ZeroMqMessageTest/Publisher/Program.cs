using Commen;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroMQ;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var context = new ZContext())
            using (var publisher = new ZSocket(context, ZSocketType.PUB))
            {
                string address = "tcp://192.168.5.70:123456";
                Console.WriteLine("I: Publisher.Bind'ing on {0}", address);
                publisher.Bind(address);


                var sendListInfo = new List<Info>();
                int conter = 0;
                while (true)
                {
                    // Get values that will fool the boss

                    conter++;

                    var id = Guid.NewGuid();
                    for (int i = 0; i < 100; i++)
                    {
                        sendListInfo.Add(new Info()
                        {
                            Conter = conter,
                            Content = String.Format("Dette er nummer {0}, med Id: {1}", conter, id),
                            Id = id
                        });
                    }

                    // Send message to all subscribers'
                    using (var message = new ZMessage())
                    {
                        message.Add(new ZFrame("temp"));
                        message.Add(new ZFrame(JsonConvert.SerializeObject(sendListInfo)));
                        publisher.SendMessage(message);
                    }
                    Console.WriteLine(sendListInfo.Count);
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}
