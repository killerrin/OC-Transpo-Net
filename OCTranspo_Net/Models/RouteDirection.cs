using Newtonsoft.Json;

namespace OCTranspo_Net.Models
{

    public class RouteDirection
    {
        [JsonProperty("RouteNo")]
        public string RouteNo { get; set; }

        [JsonProperty("RouteLabel")]
        public string RouteLabel { get; set; }
        
        [JsonProperty("Direction")]
        public string Direction { get; set; }
        
        [JsonProperty("Error")]
        public string Error { get; set; }
        
        [JsonProperty("RequestProcessingTime")]
        public string RequestProcessingTime { get; set; }

        [JsonProperty("Trips")]
        public Trips Trips { get; set; }
    }
}
