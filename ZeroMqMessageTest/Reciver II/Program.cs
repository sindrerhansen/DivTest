using Commen;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroMQ;

namespace Reciver_II
{
    class Program
    {
        private const string AppConfigPublishEndpointKey = "MessageBusPublishEndpoint";
        static void Main(string[] args)
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Any(k => k == AppConfigPublishEndpointKey))
                throw new Exception(AppConfigPublishEndpointKey + " setting is missing in app.config");

            var endpointUrlPublisher = ConfigurationManager.AppSettings[AppConfigPublishEndpointKey];
            Console.WriteLine("Sending on " + endpointUrlPublisher);

            // Socket to talk to server
            using (var context = new ZContext())
            using (var subscriber = new ZSocket(context, ZSocketType.SUB))
            {
                string connect_to = endpointUrlPublisher;
                Console.WriteLine("I: Connecting to {0}…", connect_to);
                subscriber.Connect(connect_to);

                // Subscribe to zipcode
                string topic = "temp";
                Console.WriteLine("I: Subscribing to topic code {0}…", topic);
                subscriber.Subscribe(topic);

                while (true)
                {


                    using (var replyMessage = subscriber.ReceiveMessage())
                    {
                        string reply = replyMessage[1].ReadString();
                        var obj = JsonConvert.DeserializeObject<List<Info>>(reply);
                        Console.WriteLine(obj.Count);
                    }
                }

            }
        }
    }
}
