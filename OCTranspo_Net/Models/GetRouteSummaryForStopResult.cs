using Newtonsoft.Json;
using OCTranspo_Net.Converters;

namespace OCTranspo_Net.Models
{
    public class GetRouteSummaryForStopResult
    {
        [JsonProperty("StopNo")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long StopNo { get; set; }

        [JsonProperty("StopDescription")]
        public string StopDescription { get; set; }

        /// <summary>
        /// Errors if any for the request made.
        /// </summary>
        [JsonProperty("Error")]
        public string Error { get; set; }

        [JsonProperty("Routes")]
        public Routes Routes { get; set; }
    }
}
