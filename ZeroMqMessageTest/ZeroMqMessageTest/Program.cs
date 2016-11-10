using Commen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroMqMessageTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int conter = 0;
            MessagePublisher.Start();
            var infoList = new List<Info>();
            while (true)
            {
                conter++;
                
                var id = Guid.NewGuid();
                for (int i = 0; i < 10000; i++)
                {
                    infoList.Add(new Info()
                    {
                        Conter = conter,
                        Content = String.Format("Dette er nummer {0}, med Id: {1}", conter, id),
                        Id = id
                    });
                }
                Console.WriteLine(infoList.Count);
                MessagePublisher.Publish(infoList);
                
                System.Threading.Thread.Sleep(5000);
            }
        }
    }
}
