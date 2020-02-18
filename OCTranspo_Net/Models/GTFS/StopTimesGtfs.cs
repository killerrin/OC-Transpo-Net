using Newtonsoft.Json;
using OCTranspo_Net.Converters;
using System;

namespace OCTranspo_Net.Models.GTFS
{
    public class StopTimesGtfs : GtfsBase
    {
        public const string TableName = "stop_times";

        [JsonProperty("trip_id")]
        public string trip_id { get; set; }

        [JsonProperty("arrival_time")]
        public DateTimeOffset arrival_time { get; set; }

        [JsonProperty("departure_time")]
        public DateTimeOffset departure_time { get; set; }

        [JsonProperty("stop_id")]
        public string stop_id { get; set; }

        [JsonProperty("stop_sequence")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long stop_sequence { get; set; }

        [JsonProperty("pickup_type")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long pickup_type { get; set; }

        [JsonProperty("drop_off_type")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long drop_off_type { get; set; }
    }
}
