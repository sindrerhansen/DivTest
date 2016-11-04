using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_console
{


    class Program
    {
        internal static WellCommandMaps mappings = new WellCommandMaps();
        static void Main(string[] args)
        {

            var res = JsonConvert.DeserializeObject<WellCommandMaps>(File.ReadAllText("C:\\Users\\hansens\\Source\\Repos\\test\\Test console\\Test console\\file.json"));
            mappings = res;

        }

    }
}
