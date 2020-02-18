using Newtonsoft.Json;
using OCTranspo_Net.Converters;

namespace OCTranspo_Net.Models.GTFS
{
    public class StopsGtfs : GtfsBase
    {
        public const string TableName = "stops";

        [JsonProperty("stop_id")]
        public string stop_id { get; set; }

        [JsonProperty("stop_code")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long stop_code { get; set; }

        [JsonProperty("stop_name")]
        public string stop_name { get; set; }

        [JsonProperty("stop_desc")]
        public string stop_desc { get; set; }

        [JsonProperty("stop_lat")]
        public string stop_lat { get; set; }

        [JsonProperty("stop_lon")]
        public string stop_lon { get; set; }

        [JsonProperty("stop_street")]
        public string stop_street { get; set; }

        [JsonProperty("stop_city")]
        public string stop_city { get; set; }

        [JsonProperty("stop_region")]
        public string stop_region { get; set; }

        [JsonProperty("stop_postcode")]
        public string stop_postcode { get; set; }

        [JsonProperty("stop_country")]
        public string stop_country { get; set; }

        [JsonProperty("zone_id")]
        public string zone_id { get; set; }
    }
}
