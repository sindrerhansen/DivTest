using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender
{
    public class EventPublisher
    {
        Task task;
        private bool run = true;
        public EventPublisher()
        {

        }
        public void Start()
        {
            task = Task.Factory.StartNew(() =>
            {
                using (var publish = new ZPublisher("*", 5555))
                {
                    publish.MessageName = "Sensor";
                    Random rnd = new Random();
                    while (run)
                    {
                        publish.Publish(rnd.NextDouble().ToString());
                        System.Threading.Thread.Sleep(500);
                    }
                }
            });
        }

        public void Stop()
        {
            run = false;
            task.Wait();
        }

    }
}
