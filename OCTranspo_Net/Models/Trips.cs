using Newtonsoft.Json;
using System.Collections.Generic;

namespace OCTranspo_Net.Models
{
    public class Trips
    {
        [JsonProperty("Trip")]
        public List<Trip> Trip { get; set; } = new List<Trip>();
    }
}
