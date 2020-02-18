using Newtonsoft.Json;
using OCTranspo_Net.Converters;

namespace OCTranspo_Net.Models.GTFS
{
    public class Query
    {
        [JsonProperty("table")]
        public string Table { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("column")]
        public string Column { get; set; }

        [JsonProperty("value")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Value { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }
    }
}
