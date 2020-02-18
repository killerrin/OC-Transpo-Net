using Newtonsoft.Json;
using OCTranspo_Net.Converters;
using System;
using System.Text;

namespace OCTranspo_Net.Models
{
    public class Trip
    {
        [JsonProperty("TripDestination")]
        public string TripDestination { get; set; }

        [JsonProperty("TripStartTime")]
        public string TripStartTime { get; set; }

        [JsonProperty("AdjustedScheduleTime")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AdjustedScheduleTime { get; set; }

        [JsonProperty("AdjustmentAge")]
        public string AdjustmentAge { get; set; }

        [JsonProperty("LastTripOfSchedule")]
        public bool LastTripOfSchedule { get; set; }

        [JsonProperty("BusType")]
        public string BusType { get; set; }

        [JsonProperty("Latitude")]
        public string Latitude { get; set; }

        [JsonProperty("Longitude")]
        public string Longitude { get; set; }

        [JsonProperty("GPSSpeed")]
        public string GpsSpeed { get; set; }
    }
}
