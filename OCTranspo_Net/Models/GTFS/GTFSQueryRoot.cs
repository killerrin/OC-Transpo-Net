using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCTranspo_Net.Models.GTFS
{
    public class GTFSQueryRoot<T> where T : GtfsBase
    {
        [JsonProperty("Query")]
        public Query Query { get; set; }

        [JsonProperty("Gtfs")]
        public List<T> Gtfs { get; set; } = new List<T>();

        public DateTime TimeOfRequest { get; internal set; } = DateTime.Now;
        public DateTime TimeOfResponse { get; internal set; } = DateTime.Now;
    }
}
