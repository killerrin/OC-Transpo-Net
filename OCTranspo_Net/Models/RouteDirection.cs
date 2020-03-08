using Newtonsoft.Json;
using System;
using System.Globalization;

namespace OCTranspo_Net.Models
{

    public class RouteDirection
    {
        /// <summary>
        /// Route Number
        /// </summary>
        [JsonProperty("RouteNo")]
        public string RouteNo { get; set; }

        /// <summary>
        /// Route description
        /// </summary>
        [JsonProperty("RouteLabel")]
        public string RouteLabel { get; set; }

        /// <summary>
        /// Trip direction i.e. NorthBound/SouthBound
        /// </summary>
        [JsonProperty("Direction")]
        public string Direction { get; set; }

        /// <summary>
        /// Errors if any while generating data the route.
        ///  i.e. Route does not pass the stop specified
        /// </summary>
        [JsonProperty("Error")]
        public string Error { get; set; }
        
        /// <summary>
        /// Time the request was processed. This will be using the format 
        /// 'YYYYMMDDHHMISS' where HH = 24 hour format
        /// </summary>
        [JsonProperty("RequestProcessingTime")]
        public string RequestProcessingTime { get; set; }

        [JsonProperty("Trips")]
        public Trips Trips { get; set; }

        /// <summary>
        /// Gets the RequestProcessingTime parsed as a DateTime
        /// </summary>
        /// <returns>The RequestProcessingTime as a DateTime</returns>
        public DateTime GetRequestProcessingTime()
        {
            return DateTime.ParseExact(RequestProcessingTime, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        }
    }
}
