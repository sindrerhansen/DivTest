
using InfluxData.Net.Common.Enums;
using InfluxData.Net.InfluxDb;
using InfluxData.Net.InfluxDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluxDbWriteTest
{
    public class InfluxWrite
    {
        InfluxDbClient influxDbClient;
        public InfluxWrite()
        {
            influxDbClient = new InfluxDbClient("localhost:8086/", "writer", "Kaffe123", InfluxDbVersion.v_1_0_0);
        }
        public void Write(double value)
        {
            var pointToWrite = new Point()
            {
                Name = "reading", // serie/measurement/table to write into
                Tags = new Dictionary<string, object>()
                {
                    { "SensorId", 8 },
                    { "SerialNumber", "00AF123B" }
                },
                Fields = new Dictionary<string, object>()
                {
                    { "Temperature", value },
                },

            };

            influxDbClient.Client.WriteAsync("mydb", pointToWrite).Wait();
        }
    }
}
