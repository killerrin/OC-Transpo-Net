using Newtonsoft.Json;
using OCTranspo_Net.Converters;
using System.Collections.Generic;

namespace OCTranspo_Net.Models
{
    public class Route
    {
        /// <summary>
        /// Route number
        /// </summary>
        [JsonProperty("RouteNo")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long RouteNo { get; set; }

        /// <summary>
        /// Direction identifier. e.g. 0 = Eastbound, 1 = WestBound
        /// </summary>
        [JsonProperty("DirectionID")]
        public long DirectionId { get; set; }

        /// <summary>
        /// Direction description e.g. Inbound / Outbound
        /// </summary>
        [JsonProperty("Direction")]
        public string Direction { get; set; }

        /// <summary>
        /// The route and direction heading
        /// </summary>
        [JsonProperty("RouteHeading")]
        public string RouteHeading { get; set; }

        [JsonProperty("Trips")]
        public List<Trip> Trips { get; set; } = new List<Trip>();

        [JsonProperty("RouteDirection")]
        public List<RouteDirection> RouteDirection { get; set; } = new List<RouteDirection>();
    }
}
