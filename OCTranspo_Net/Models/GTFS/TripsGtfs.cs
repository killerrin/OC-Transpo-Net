using Newtonsoft.Json;
using OCTranspo_Net.Converters;

namespace OCTranspo_Net.Models.GTFS
{
    public class TripsGtfs : GtfsBase
    {
        public const string TableName = "trips";

        [JsonProperty("route_id")]
        public string route_id { get; set; }

        [JsonProperty("service_id")]
        public string service_id { get; set; }

        [JsonProperty("trip_id")]
        public string trip_id { get; set; }

        [JsonProperty("trip_headsign")]
        public string trip_headsign { get; set; }

        [JsonProperty("block_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long block_id { get; set; }
    }
}
