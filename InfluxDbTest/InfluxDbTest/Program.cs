using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluxDbWriteTest
{
    class Program
    {
        
        static void Main(string[] args)
        {
            bool run = true;
            var iW = new InfluxWrite();
            while (run)
            {
                Console.WriteLine("Give me a value (q for quit):");
                var value = Console.ReadLine();
                if (value == "q")
                {
                    return;
                }
                var sendValue = double.Parse(value);
                iW.Write(sendValue);
            }        
        }
    }
}
