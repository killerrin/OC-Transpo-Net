using Newtonsoft.Json;
using System;

namespace OCTranspo_Net.Models
{
    public class GetNextTripsForStopResultRoot
    {
        [JsonProperty("GetNextTripsForStopResult")]
        public GetNextTripsForStopResult GetNextTripsForStopResult { get; set; }

        public DateTime TimeOfRequest{ get; internal set; } = DateTime.Now;
        public DateTime TimeOfResponse { get; internal set; } = DateTime.Now;
    }
}
