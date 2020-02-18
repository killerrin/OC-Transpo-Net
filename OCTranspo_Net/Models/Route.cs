using Newtonsoft.Json;
using OCTranspo_Net.Converters;
using System.Collections.Generic;

namespace OCTranspo_Net.Models
{
    public class Route
    {
        [JsonProperty("RouteNo")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long RouteNo { get; set; }

        [JsonProperty("DirectionID")]
        public long DirectionId { get; set; }

        [JsonProperty("Direction")]
        public string Direction { get; set; }

        [JsonProperty("RouteHeading")]
        public string RouteHeading { get; set; }

        [JsonProperty("Trips")]
        public Trip[] Trips { get; set; }

        [JsonProperty("RouteDirection")]
        public List<RouteDirection> RouteDirection { get; set; }
    }
}
