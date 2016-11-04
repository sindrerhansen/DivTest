using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_console
{
    public class Mapping
    {
        public string Tag { get; set; }
        public string Address { get; set; }
    }

    public class WellMap
    {
        public string Map { get; set; }
        public List<Mapping> Mappings { get; set; }
    }

    public class WellCommandMaps
    {
        public List<WellMap> WellMaps { get; set; }
    }
    public WellMap GetWellMap(WellSenter wellCenter)
    {

        return mappings.WellMaps.FirstOrDefault(o => o.Map == wellCenter.ToString());
    }
    public Mapping GetMapping(TongSignals songSignal, WellSenter wellSenter)
    {
        foreach (var item in mappings.WellMaps)
        {
            try
            {
                var i = item.Mappings.First(x => x.Tag == songSignal.ToString());
                return i;
            }
            catch
            {
                return null;
            }
        }
        return null;
    }
}
