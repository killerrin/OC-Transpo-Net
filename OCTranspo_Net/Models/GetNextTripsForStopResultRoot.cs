using Newtonsoft.Json;

namespace OCTranspo_Net.Models
{
    public class GetNextTripsForStopResultRoot
    {
        [JsonProperty("GetNextTripsForStopResult")]
        public GetNextTripsForStopResult GetNextTripsForStopResult { get; set; }
    }
}
