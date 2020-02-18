using Newtonsoft.Json;
using OCTranspo_Net.Converters;

namespace OCTranspo_Net.Models.GTFS
{
    public class CalendarGtfs : GtfsBase
    {
        public const string TableName = "calendar";

        [JsonProperty("service_id")]
        public string service_id { get; set; }

        [JsonProperty("monday")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long monday { get; set; }

        [JsonProperty("tuesday")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long tuesday { get; set; }

        [JsonProperty("wednesday")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long wednesday { get; set; }

        [JsonProperty("thursday")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long thursday { get; set; }

        [JsonProperty("friday")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long friday { get; set; }

        [JsonProperty("saturday")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long saturday { get; set; }

        [JsonProperty("sunday")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long sunday { get; set; }

        [JsonProperty("start_date")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long start_date { get; set; }

        [JsonProperty("end_date")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long end_date { get; set; }
    }
}
