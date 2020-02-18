using Newtonsoft.Json;

namespace OCTranspo_Net.Models
{

    public class GetNextTripsForStopResult
    {
        [JsonProperty("StopNo")]
        public string StopNo { get; set; }

        [JsonProperty("StopLabel")]
        public string StopLabel { get; set; }

        [JsonProperty("Error")]
        public string Error { get; set; }

        [JsonProperty("Route")]
        public Route Route { get; set; }
    }
}
