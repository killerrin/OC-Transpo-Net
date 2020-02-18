using Newtonsoft.Json;
using OCTranspo_Net.Converters;

namespace OCTranspo_Net.Models.GTFS
{
    public class RoutesGtfs : GtfsBase
    {
        public const string TableName = "routes";

        [JsonProperty("route_id")]
        public string route_id { get; set; }

        [JsonProperty("route_short_name")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long route_short_name { get; set; }

        [JsonProperty("route_long_name")]
        public string route_long_name { get; set; }

        [JsonProperty("route_desc")]
        public string route_desc { get; set; }

        [JsonProperty("route_type")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long route_type { get; set; }
    }
}
