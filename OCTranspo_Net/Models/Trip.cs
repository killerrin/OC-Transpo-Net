using Newtonsoft.Json;
using OCTranspo_Net.Converters;
using System;
using System.Globalization;
using System.Text;

namespace OCTranspo_Net.Models
{
    public class Trip
    {
        /// <summary>
        /// Final stop on the trip
        /// </summary>
        [JsonProperty("TripDestination")]
        public string TripDestination { get; set; }

        /// <summary>
        /// start time for the trip. Format HH:MI, where HH = 24 hour format
        /// </summary>
        /// <remarks>
        /// TripStartTime is the scheduled time the run was supposed to leave the first stop.
        /// It's only really useful for determining which bus corresponds to which run in a full timetable.
        /// @https://www.reddit.com/r/ottawa/comments/epms7d/question_for_programmers_using_the_octranspo_api/femw9sr/
        /// </remarks>
        [JsonProperty("TripStartTime")]
        public string TripStartTime { get; set; }

        /// <summary>
        /// adjusted scheduled time in minutes
        /// </summary>
        /// <remarks>
        /// AdjustedScheduleTime is the number of minutes after the time of the request (which is not included in the response)
        /// that the bus is presumed to arrive, assuming it does not gain or lose time from where it is until it arrives at the stop.
        /// For instance, if the scheduled time from Stop A to Stop B is exactly 15 minutes, then any bus located at
        /// Stop A (no matter how early or late he was in arriving at Stop A, or how slow or fast the drive to Stop B will be in reality)
        /// will be AdjustedScheduleTime "15" in a data request for Stop B.
        /// @https://www.reddit.com/r/ottawa/comments/epms7d/question_for_programmers_using_the_octranspo_api/femw9sr/
        /// </remarks>
        [JsonProperty("AdjustedScheduleTime")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AdjustedScheduleTime { get; set; }

        /// <summary>
        /// The time since the scheduled was adjusted in whole and fractional minutes.
        /// </summary>
        [JsonProperty("AdjustmentAge")]
        public double AdjustmentAge { get; set; }

        /// <summary>
        /// last trip to pass the stop for the route & direction
        /// </summary>
        [JsonProperty("LastTripOfSchedule")]
        public bool LastTripOfSchedule { get; set; }

        /// <summary>
        /// type of bus : low floor, bike rack etc.
        /// </summary>
        [JsonProperty("BusType")]
        public string BusType { get; set; }

        /// <summary>
        /// Latitude of the last gps reading for the bus
        /// </summary>
        [JsonProperty("Latitude")]
        public string Latitude { get; set; }

        /// <summary>
        /// Longitude of the last gps reading for the bus
        /// </summary>
        [JsonProperty("Longitude")]
        public string Longitude { get; set; }

        /// <summary>
        /// speed of the bus in km/hr
        /// </summary>
        [JsonProperty("GPSSpeed")]
        public string GpsSpeed { get; set; }

        /// <summary>
        /// Gets the arrival time as a conversion of the Trip Start Time
        /// </summary>
        /// <returns>The Start Trip Time as a DateTime</returns>
        public DateTime GetArrivalTime()
        {
            var parts = TripStartTime.Split(':');
            var hours = int.Parse(parts[0]);
            var minutes = int.Parse(parts[1]);
            var startTime = new TimeSpan(hours, minutes, 0);

            return new DateTime().Add(startTime).AddMinutes(AdjustedScheduleTime);
        }

        /// <summary>
        /// Gets the Arrival Time as based upon the API Spec: 
        /// - The TripStartTime if AdjustmentAge is less than 0
        /// - The Adjusted Scheduled Time if AdjustmentAge is less than 0
        /// </summary>
        /// <param name="timeOfRequest">The Time that the API request was made</param>
        /// <returns>The Time that the trip should arrive</returns>
        public DateTime GetArrivalTime(DateTime timeOfRequest)
        {
            if (AdjustmentAge < 0) { return GetArrivalTime(); }
            return GetAdjustedArrivalTime(timeOfRequest);
        }

        /// <summary>
        /// Gets the Arrival Time taking into account the Adjusted Schedule Time
        /// </summary>
        /// <param name="timeOfRequest">The Time that the API request was made</param>
        /// <returns>The Time that the trip should arrive</returns>
        public DateTime GetAdjustedArrivalTime(DateTime timeOfRequest)
        {
            return timeOfRequest.AddMinutes(AdjustedScheduleTime);
        }
    }
}
