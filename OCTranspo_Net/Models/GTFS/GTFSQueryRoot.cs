using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace OCTranspo_Net.Models.GTFS
{
    public class GTFSQueryRoot<T> where T : GtfsBase
    {
        [JsonProperty("Query")]
        public Query Query { get; set; }

        [JsonProperty("Gtfs")]
        public T[] Gtfs { get; set; }
    }
}
