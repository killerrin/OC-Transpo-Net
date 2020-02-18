using Newtonsoft.Json;
using OCTranspo_Net.Converters;

namespace OCTranspo_Net.Models.GTFS
{
    public class CalendarDatesGtf : GtfsBase
    {
        public const string TableName = "calendar_dates";

        [JsonProperty("service_id")]
        public string service_id { get; set; }

        [JsonProperty("date")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long date { get; set; }

        [JsonProperty("exception_type")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long exception_type { get; set; }
    }
}
