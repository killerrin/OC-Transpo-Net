using Newtonsoft.Json;
using System.Collections.Generic;

namespace OCTranspo_Net.Models
{
    public class Routes
    {
        [JsonProperty("Route")]
        public List<Route> Route { get; set; } = new List<Route>();
    }
}
