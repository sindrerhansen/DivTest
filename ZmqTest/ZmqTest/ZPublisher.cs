using System;
using ZeroMQ;

namespace Sender
{
    public class ZPublisher : IDisposable
    {
        
        ZSocket publisher = new ZSocket(ZSocketType.PUB);
        public ZPublisher(string ipAddress, int port)
        {
            var address = String.Format("tcp://{0}:{1}", ipAddress, port);
            publisher.Bind(address);
        }
        public string MessageName { get; set; }


        public void Publish(string chanalName, string value)
        {
            MessageName = chanalName;
            Send(value);
        }
        public void Publish(string value)
        {
            Send(value);
        }
        public void Dispose()
        {
            publisher.Dispose();
        }

        private void Send(string value)
        {
            
            using (var message = new ZMessage())
            {
                message.Add(new ZFrame(MessageName));
                message.Add(new ZFrame(value));
                publisher.Send(message);
            }
        }
    }
}
