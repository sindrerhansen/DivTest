using Commen;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroMQ;

namespace Reciver
{
    class Program
    {
        
        private const string AppConfigSubscriberEndpointKey = "MessageBusSubscriberEndpoint";
        static void Main(string[] args)
        {


            if (!ConfigurationManager.AppSettings.AllKeys.Any(k => k == AppConfigSubscriberEndpointKey))
                throw new Exception(AppConfigSubscriberEndpointKey + " setting is missing in app.config");
            
            var endpointUrlSubscriber = ConfigurationManager.AppSettings[AppConfigSubscriberEndpointKey];
            using (var context = new ZContext())
            using (var subscriber = new ZSocket(context, ZSocketType.SUB))
            {
                subscriber.Connect(endpointUrlSubscriber);
                subscriber.Subscribe("info");
                Console.WriteLine("starting lissening on: " + endpointUrlSubscriber);
                string msgPayload = "";
                while (true)
                {

                using (ZMessage message = subscriber.ReceiveMessage())
                {

                        try
                        {
                            var msgType = message[0].ReadString();
                            if (message[1].CanRead)
                            {
                                msgPayload = message[1].ReadLine();
                                var deserializedMessage = JsonConvert.DeserializeObject<List<Info>>(msgPayload);

                                Console.WriteLine(deserializedMessage.Count);
                            }

                            

                    
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex);
                        }

                }
                    

            System.Threading.Thread.Sleep(5);
                }
            }
        }
    }
}
