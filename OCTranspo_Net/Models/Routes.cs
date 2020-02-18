using Newtonsoft.Json;

namespace OCTranspo_Net.Models
{
    public class Routes
    {
        [JsonProperty("Route")]
        public Route[] Route { get; set; }
    }
}
